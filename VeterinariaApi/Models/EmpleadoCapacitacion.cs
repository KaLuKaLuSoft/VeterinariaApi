using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinariaApi.Models
{
    public class EmpleadoCapacitacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        [ForeignKey("EmpleadoId")]
        public Empleados? Empleado { get; set; }
        public int CapacitacionId { get; set; }
        [ForeignKey("CapacitacionId")]
        public CursoCapacitacion? CursoCapacitacion { get; set; }
        public DateTime? Fecha_Capacitacion { get; set; }
        public string? EstadoAprobacion { get; set; } // Aprobado, Rechazado, Pendiente
        public decimal? Calificacion { get; set; }
        public bool? Activo { get; set; } = false;
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
