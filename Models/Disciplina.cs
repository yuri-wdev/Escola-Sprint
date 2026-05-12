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

    }
}
