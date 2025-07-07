using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinariaApi.Models
{
    public class EmpleadoEsepecialidad
    {
        public int EmpleadoId { get; set; }
        [ForeignKey("EmpleadoId")]
        public Empleados? Empleado { get; set; }
        public int EspecialidadId { get; set; } 
        [ForeignKey("EspecialidadId")]
        public EspecialidadesMedicas? Especialidad { get; set; }
        public DateTime? FechaCertificacion { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
