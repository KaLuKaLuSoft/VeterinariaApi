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

        public PlanSuscripcion PlanSuscripcion { get; set; } = PlanSuscripcion.Basico;

        public EstadoCuenta EstadoCuenta { get; set; } = EstadoCuenta.Activo;

        public DateTime FechaRegistro { get; set; }

        public int IdCiudad { get; set; }
        public string? NombreCiudad { get; set; }
    }
}
