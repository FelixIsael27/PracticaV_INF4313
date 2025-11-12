using System.ComponentModel.DataAnnotations;

namespace Actividad5LengProg3.Models
{
    public class CarreraViewModel
    {
        [Key]
        [Required(ErrorMessage = "El campo Código es obligatorio.")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La cantidad de créditos es obligatoria.")]
        [Range(1, 300, ErrorMessage = "Debe ser un número positivo.")]
        public int CantidadCreditos { get; set; }

        [Required(ErrorMessage = "La cantidad de materias es obligatoria.")]
        [Range(1, 100, ErrorMessage = "Debe ser un número positivo.")]
        public int CantidadMaterias { get; set; }

        public ICollection<EstudianteViewModel> Estudiantes { get; set; }
    }
}
