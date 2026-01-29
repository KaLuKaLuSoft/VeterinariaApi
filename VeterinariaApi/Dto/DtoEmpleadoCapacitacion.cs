using VeterinariaApi.Models;

namespace VeterinariaApi.Dto
{
    public class DtoEmpleadoCapacitacion
    {
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public string? Empleado { get; set; }
        public int CapacitacionId { get; set; }
        public string? CursoCapacitacion { get; set; }
        public DateTime? Fecha_Capacitacion { get; set; }
        public string? EstadoAprobacion { get; set; }
        public decimal? Calificacion { get; set; }
        public bool? Activo { get; set; } = false;
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
