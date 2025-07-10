using System.ComponentModel.DataAnnotations.Schema;
using VeterinariaApi.Models;

namespace VeterinariaApi.Dto
{
    public class DtoTurnosEmpleado
    {
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public string? NombreEmpleado { get; set; }
        public int SucursalId { get; set; }
        public string? NombreSucursal { get; set; }
        public int TurnoId { get; set; }
        public string? NombreTurno { get; set; }
        public DateTime? Fecha { get; set; }
        public bool Confirmado { get; set; } = false;
        public string? Observaciones { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
