using System.ComponentModel.DataAnnotations;

namespace ExamenPrueba2.Model.ViewModel
{
    public class RandomViewModel
    {
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo no puede tener más de 50 caracteres.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "El campo solo puede contener caracteres alfanuméricos.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Coloca un número entero válido.")]
        [Range(1, int.MaxValue, ErrorMessage = "El Multiplo debe ser un número positivo.")]
        public int Multiplo { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Coloca un número entero válido.")]
        [Range(1, 15, ErrorMessage = "El valor debe estar entre 1 y 15.")]
        public int Frecuencia { get; set; }
    }
}
