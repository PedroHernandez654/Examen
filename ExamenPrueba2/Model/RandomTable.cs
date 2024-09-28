using System.ComponentModel.DataAnnotations;

namespace ExamenPrueba2.Model
{
    public class RandomTable
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Multiplo { get; set; }
        public int Frecuencia { get; set; }
        public int NumeroRandom { get; set; }
    }
}
