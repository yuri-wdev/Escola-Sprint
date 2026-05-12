using System.ComponentModel.DataAnnotations.Schema;

namespace Escola_Sprint.Models
{
    public class Aluno
    {
        public int IdAluno { get; set; }
        public string Nome { get; set; } = null!;
        public string Sexo { get; set; } = null!;

        public int IdCurso { get; set; }
        public virtual Curso Curso { get; set; } = null!;

        public virtual ICollection<Notas> Notas { get; set; } = new List<Notas>(); 
        public virtual ICollection<Disciplina> Disciplinas { get; set; } = new List<Disciplina>();
    }
}
