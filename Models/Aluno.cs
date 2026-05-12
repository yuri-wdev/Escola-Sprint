using System.ComponentModel.DataAnnotations.Schema;

namespace Escola_Sprint.Models
{
    public class Aluno
    {
        public int IdAluno { get; set; }
        public string Nome { get; set; } = null!;
        public string Sexo { get; set; } = null!;
        public int Curso { get; set; }

    }
}
