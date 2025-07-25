using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinariaApi.Models
{
    public class EvaluacionEmpleado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public int CriterioEvaluacionId { get; set; }
        public DateTime? Fecha_Evaluacion { get; set; }
        public decimal? Calificacion { get; set; }
        public string? Comentarios { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
