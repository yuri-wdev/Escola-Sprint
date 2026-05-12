using Microsoft.EntityFrameworkCore;
using Escola_Sprint.Models;

namespace Escola_Sprint.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Notas> Notas { get; set; }
        public DbSet<Professor> Professores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Aluno (Ajustado para 'aluno' conforme seu SQL)
            modelBuilder.Entity<Aluno>(entity =>
            {
                entity.ToTable("aluno");
                entity.HasKey(e => e.IdAluno);

                entity.HasOne(a => a.Curso)
                      .WithMany(c => c.Alunos)
                      .HasForeignKey(a => a.IdCurso)
                      .HasConstraintName("fk_Aluno_Cursos");
            });

            // 2. Disciplinas x Cursos (N:N)
            modelBuilder.Entity<Disciplina>()
                .HasMany(d => d.Cursos)
                .WithMany(c => c.Disciplinas)
                .UsingEntity<Dictionary<string, object>>(
                    "disciplinas_has_cursos",
                    j => j.HasOne<Curso>().WithMany().HasForeignKey("Cursos_idCurso"),
                    j => j.HasOne<Disciplina>().WithMany().HasForeignKey("Disciplinas_idDisciplina")
                );

            // 3. Aluno x Disciplinas (N:N) - Faltava este!
            modelBuilder.Entity<Aluno>()
                .HasMany(a => a.Disciplinas)
                .WithMany(d => d.Alunos)
                .UsingEntity<Dictionary<string, object>>(
                    "aluno_has_disciplinas",
                    j => j.HasOne<Disciplina>().WithMany().HasForeignKey("Disciplinas_idDisciplina"),
                    j => j.HasOne<Aluno>().WithMany().HasForeignKey("Aluno_idAluno")
                );

            // 4. Notas (Corrigido o erro do 'entity')
            modelBuilder.Entity<Notas>(entity =>
            {
                entity.ToTable("notas");
                entity.HasKey(e => e.IdRelatorio);
                entity.Property(e => e.IdRelatorio).HasColumnName("idRelatorio");

                entity.Property(e => e.Nota1).HasColumnName("Nota_1");
                entity.Property(e => e.Nota2).HasColumnName("Nota_2");
                entity.Property(e => e.Nota3).HasColumnName("Nota_3");
                entity.Property(e => e.AnoLetivo).HasColumnName("Ano_letivo");

                entity.HasOne(n => n.Aluno)
                      .WithMany(a => a.Notas)
                      .HasForeignKey(n => n.AlunoId)
                      .HasConstraintName("fk_Notas_Aluno1");

                entity.HasOne(n => n.Disciplina)
                      .WithMany(d => d.Notas)
                      .HasForeignKey(n => n.DisciplinaId)
                      .HasConstraintName("fk_Notas_Disciplinas1");
            });

            // 5. Disciplina x Professor (1:N)
            modelBuilder.Entity<Disciplina>()
                .HasOne(d => d.Professor)
                .WithMany(p => p.Disciplinas)
                .HasForeignKey(d => d.CodigoProfessor)
                .HasConstraintName("fk_disciplinas_professor1");
        }
    }
}