using System.ComponentModel.DataAnnotations;

namespace Practica2.Models;

public class Curso : IValidatableObject
{
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    public string Codigo { get; set; } = string.Empty;

    [Required]
    [StringLength(150)]
    public string Nombre { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Los créditos deben ser mayores a 0.")]
    public int Creditos { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "El cupo maximo debe ser mayor a 0.")]
    public int CupoMaximo { get; set; }

    public TimeOnly HorarioInicio { get; set; }

    public TimeOnly HorarioFin { get; set; }

    public bool Activo { get; set; }

    public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Creditos <= 0)
        {
            yield return new ValidationResult(
                "Los créditos deben ser mayores a 0.",
                new[] { nameof(Creditos) });
        }

        // Requerimiento: HorarioInicio < HorarioFin
        if (HorarioInicio >= HorarioFin)
        {
            yield return new ValidationResult(
                "HorarioInicio debe ser anterior a HorarioFin.",
                new[] { nameof(HorarioInicio), nameof(HorarioFin) });
        }
    }
}
