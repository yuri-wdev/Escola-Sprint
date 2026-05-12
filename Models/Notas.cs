using Escola_Sprint.Models;

public class Notas
{
    public int IdRelatorio { get; set; }
    public float Nota1 { get; set; }
    public float Nota2 { get; set; }
    public float Nota3 { get; set; }
    public int AnoLetivo { get; set; }

    public int AlunoId { get; set; }
    public virtual Aluno Aluno { get; set; } = null!;

    public int DisciplinaId { get; set; }
    public virtual Disciplina Disciplina { get; set; } = null!;
}