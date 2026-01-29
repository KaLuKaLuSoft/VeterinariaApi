using System.ComponentModel.DataAnnotations.Schema;
using VeterinariaApi.Models;

namespace VeterinariaApi.Dto
{
    public class DtoMovimientosNomina
    {
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public string? Empleado { get; set; }
        public int ConceptoNominaId { get; set; }
        public DateTime? FechaMovimiento { get; set; }
        public decimal? Monto { get; set; }
        public string? PeriodoNomina { get; set; }
        public int RegistradorPorEmpleado { get; set; }
        public string? RegistradorPorEmpleados { get; set; }
        public string? Observaciones { get; set; }
        public bool? Activo { get; set; } = false;
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
