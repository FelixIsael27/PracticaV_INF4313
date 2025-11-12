using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Actividad5LengProg3.Models
{
    public class EstudianteViewModel
    {
        [Key]
        [Required(ErrorMessage = "La Matrícula es obligatoria")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "La matrícula debe tener entre 6 y 15 caracteres")]
        public string Matricula { get; set; }

        [Required(ErrorMessage = "El Nombre Completo es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "Seleccione la Carrera")]
        public string Carrera { get; set; }

        [ForeignKey(nameof(Carrera))]
        public CarreraViewModel CarreraCod { get; set; }

        [Required(ErrorMessage = "Seleccione el Recinto")]
        public string Recinto { get; set; }

        [ForeignKey(nameof(Recinto))]
        public RecintoViewModel RecintoCod { get; set; }

        [Required(ErrorMessage = "El Correo Institucional es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        [Display(Name = "Correo Institucional")]
        public string CorreoInstitucional { get; set; }

        [Phone(ErrorMessage = "Teléfono celular inválido")]
        [MinLength(10, ErrorMessage = "El celular debe tener al menos 10 dígitos")]
        [Display(Name = "Celular")]
        public string Celular { get; set; }

        [Phone(ErrorMessage = "Teléfono fijo inválido")]
        [MinLength(10, ErrorMessage = "El teléfono debe tener al menos 10 dígitos")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "La Dirección es obligatoria")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "La Fecha de Nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime? FechaNacimiento { get; set; }

        [Required(ErrorMessage = "Seleccione el Género")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "Seleccione el Turno")]
        public string Turno { get; set; }

        [Display(Name = "¿Está becado?")]
        public bool EstaBecado { get; set; }

        [Range(0, 100, ErrorMessage = "Porcentaje debe ser entre 0 y 100")]
        [Display(Name = "Porcentaje de Beca")]
        public int? PorcentajeBeca { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EstaBecado)
            {
                if (PorcentajeBeca == null)
                {
                    yield return new ValidationResult("Ingrese el porcentaje de beca: ", new[] { nameof(PorcentajeBeca) });
                }
                else if (PorcentajeBeca < 0 || PorcentajeBeca > 100)
                {
                    yield return new ValidationResult("El Porcentaje de la beca debe estar entre 0 y 100", new[] { nameof(PorcentajeBeca) });
                }
            }
        }
    }
}
