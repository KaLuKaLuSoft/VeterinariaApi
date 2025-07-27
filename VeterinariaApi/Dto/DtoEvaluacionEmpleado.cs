using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VeterinariaApi.Models;

namespace VeterinariaApi.Dto
{
    public class DtoEvaluacionEmpleado
    {
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public string? Empleado { get; set; }
        public int CriterioEvaluacionId { get; set; }
        public string? CriterioEvaluacion { get; set; }
        public DateTime? Fecha_Evaluacion { get; set; }
        public decimal? Calificacion { get; set; }
        public string? Comentarios { get; set; }
        public int EvaluadoPorEmpleadoId { get; set; }
        public string? EmpleadoEvaluador { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
