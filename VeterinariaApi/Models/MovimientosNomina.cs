using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinariaApi.Models
{
    public class MovimientosNomina
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        [ForeignKey("EmpleadoId")]
        public Empleados? Empleado { get; set; }
        public int ConceptoNominaId { get; set; }
        public DateTime? FechaMovimiento { get; set; }
        public decimal? Monto { get; set; }
        public string? PeriodoNomina { get; set; }
        public int RegistradorPorEmpleado { get; set; }
        [ForeignKey("RegistradorPorEmpleado")]
        public Empleados? RegistradorPorEmpleados { get; set; }
        public string? Observaciones { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
