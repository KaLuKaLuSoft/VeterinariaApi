using VeterinariaApi.Models;

namespace VeterinariaApi.Dto
{
    public class DtoEmpresa
    {
        public int Id { get; set; }

        public string? NombreComercial { get; set; }

        public string? RazonSocial { get; set; }

        public int? NumeroTrabajadores { get; set; }

        public string? Nit { get; set; }

        public string? LogoUrl { get; set; }

        public PlanSuscripcion PlanSuscripcion { get; set; } = PlanSuscripcion.Demo;

        public EstadoCuenta EstadoCuenta { get; set; } = EstadoCuenta.Activo;

        public DateTime FechaRegistro { get; set; }

        public int IdPais { get; set; }
        public string? NombrePais { get; set; }
    }
}
