using System.ComponentModel.DataAnnotations;

namespace Actividad5LengProg3.Models
{
    public class RecintoViewModel
    {
        [Key]
        [Required(ErrorMessage = "El campo Código es obligatorio.")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no debe exceder mas de 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Dirección es obligatorio.")]
        [StringLength(100, ErrorMessage = "La dirección no debe exceder mas de 100 caracteres.")]
        public string Direccion { get; set; }

        public ICollection<EstudianteViewModel> Estudiantes { get; set; }
    }
}
