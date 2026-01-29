using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinariaApi.Dto
{
    public class DtoAusenciaEmpleado
    {
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public string? Empleado { get; set; }
        public int TipoAusenciaId { get; set; }
        public string? TipoAusencia { get; set; }
        public string? Estado { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? Motivo { get; set; }
        public int AprobadoPorEmpleado { get; set; }
        public string? AprobadoPorEmpleados { get; set; }
        public bool? Activo { get; set; } = false;
        public DateTime? FechaSolicitud { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
