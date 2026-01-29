using System.ComponentModel.DataAnnotations.Schema;
using VeterinariaApi.Models;

namespace VeterinariaApi.Dto
{
    public class DtoEmpleadoEspecialidad
    {
        public int EmpleadoId { get; set; }
        public int EspecialidadId { get; set; }
        public bool? Activo { get; set; } = false;
        public DateTime? FechaCertificacion { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
