using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Escola_Sprint.Models
{
    public class Curso
    {
        [Key]
        public int IdCurso { get; set; }

        public string Nome { get; set; } = null!;

        public int CH_Total { get; set; }

        public int CH_Semanal {  get; set; }

        [JsonIgnore]
        public virtual ICollection<Aluno> Alunos { get; set; } = new List<Aluno>();

        [JsonIgnore]
        public virtual ICollection<Disciplina> Disciplinas { get; set; } = new List<Disciplina>();
    }
}

