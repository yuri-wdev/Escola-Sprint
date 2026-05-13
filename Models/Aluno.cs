using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Escola_Sprint.Models
{
    public class Aluno
    {
        public int IdAluno { get; set; }
        public string Nome { get; set; } = null!;
        public string Sexo { get; set; } = null!;

        public int IdCurso { get; set; }

        [JsonIgnore]
        public virtual Curso? Curso { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Notas> Notas { get; set; } = new List<Notas>();

        [JsonIgnore]
        public virtual ICollection<Disciplina> Disciplinas { get; set; } = new List<Disciplina>();
    }
}
