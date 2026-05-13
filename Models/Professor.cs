using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Escola_Sprint.Models
{
    public class Professor
    {
        [Key]
        public int Id {  get; set; }
        public string Nome { get; set; } = null!;
        public string Sexo { get; set; } = null!;
        public string Titularidade { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Disciplina> Disciplinas { get; set; } = new List<Disciplina>();

    }
}
