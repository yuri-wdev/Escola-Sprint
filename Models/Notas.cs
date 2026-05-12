namespace Escola_Sprint.Models
{
    public class Notas
    {
        public int Id { get; set; }
        public float Nota_1 { get; set; }
        public float Nota_2 { get; set; }
        public float Nota_3 { get; set; }
        public int IdAluno {  get; set; }
        public int IdDisciplina { get; set; }
        public int Anoletivo { get; set; }

    }
}
