using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinariaApi.Models
{
    public class Empresa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? NombreComercial { get; set; }
        public string? RazonSocial { get; set; }
        public int? NumeroTrabajadores { get; set; }
        public string? Nit { get; set; }

        [MaxLength(500)]
        public string? LogoUrl { get; set; }

        public PlanSuscripcion PlanSuscripcion { get; set; } = PlanSuscripcion.Basico;

        public EstadoCuenta EstadoCuenta { get; set; } = EstadoCuenta.Activo;

        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        public int IdPais { get; set; }
        [ForeignKey("IdPais")]
        public Paises? NombrePais { get; set; }
    }

    public enum PlanSuscripcion
    {
        Demo,
        Basico,
        Pro,
        Premium
    }

    public enum EstadoCuenta
    {
        Activo,
        Suspendido,
        Vencido
    }
}
