using System.ComponentModel.DataAnnotations;

namespace PawCare.Models
{
    public class Mascota
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la mascota es obligatorio.")]
        public string NombreMascota { get; set; }

        [Required(ErrorMessage = "El nombre del dueño es obligatorio.")]
        public string NombreDueno { get; set; }

        [Required(ErrorMessage = "Seleccione un tipo.")]
        public string Tipo { get; set; }

        [Range(0, 30, ErrorMessage = "La edad debe estar entre 0 y 30 años.")]
        public int Edad { get; set; }

        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "El teléfono debe tener 9 dígitos.")]
        public string Telefono { get; set; }

        public string Observaciones { get; set; }
    }
}