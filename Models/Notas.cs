using Escola_Sprint.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Notas
{
    [Key]
    public int IdRelatorio { get; set; }
    public float Nota1 { get; set; }
    public float Nota2 { get; set; }
    public float Nota3 { get; set; }
    public int AnoLetivo { get; set; }

    public int AlunoId { get; set; }

    [JsonIgnore]
    public virtual Aluno Aluno { get; set; } = null!;

    public int DisciplinaId { get; set; }

    [JsonIgnore]
    public virtual Disciplina Disciplina { get; set; } = null!;
}