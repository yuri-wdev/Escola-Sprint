using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Escola_Sprint.Models
{
    public class Disciplina
    {
        [Key]
        public int IdDisciplina { get; set; }

        public int CH { get; set; }
        
        public string Titulo { get; set; } = null!;

        public int codigoprofessor { get; set; }

        [JsonIgnore]
        public virtual Professor Professor { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Curso> Cursos { get; set; } = new List<Curso>();

        [JsonIgnore]
        public virtual ICollection<Aluno> Alunos { get; set; } = new List<Aluno>();

        [JsonIgnore]
        public virtual ICollection<Notas> Notas { get; set; } = new List<Notas>();

    }
}
