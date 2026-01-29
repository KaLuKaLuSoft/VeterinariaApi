using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinariaApi.Models
{
    public class TurnosEmpleado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        [ForeignKey("EmpleadoId")]
        public Empleados? Empleados { get; set; }
        public int SucursalId { get; set; }
        [ForeignKey("SucursalId")]
        public Sucursales? Sucursales { get; set; }
        public int TurnoId { get; set; }
        [ForeignKey("TurnoId")]
        public TipoTurno? Turnos { get; set; }
        public DateTime? Fecha { get; set; }
        public bool Confirmado { get; set; } = false;
        public bool? Activo { get; set; } = false;
        public string? Observaciones { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
