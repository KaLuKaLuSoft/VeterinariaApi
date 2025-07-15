using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinariaApi.Models
{
    public class AusenciaEmpleado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        [ForeignKey("EmpleadoId")]
        public Empleados? Empleado { get; set; }
        public int TipoAusenciaId { get; set; }
        [ForeignKey("TipoAusenciaId")]
        public TipoAusencia? TipoAusencia { get; set; }
        public string? Estado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string? Motivo { get; set; }
        public int AprobadoPorEmpleado { get; set; }
        [ForeignKey("AprobadoPorEmpleado")]
        public Empleados? AprobadoPorEmpleados { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaAprobacion { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
    /* Estado
    Pendiente
    Aprobada
    Rechazada
    Cancelada
    */
}
