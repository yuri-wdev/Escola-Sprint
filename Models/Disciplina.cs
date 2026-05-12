using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Escola_Sprint.Models
{
    public class Disciplina
    {
        public int IdDisciplina { get; set; }

        public int CH { get; set; }
        
        public string Titulo { get; set; } = null!;

        public int codigoprofessor { get; set; }

        public int CodigoProfessor { get; set; }
        public virtual Professor Professor { get; set; } = null!;

        public virtual ICollection<Curso> Cursos { get; set; } = new List<Curso>();
        public virtual ICollection<Aluno> Alunos { get; set; } = new List<Aluno>();

        public virtual ICollection<Notas> Notas { get; set; } = new List<Notas>();

    }
}
