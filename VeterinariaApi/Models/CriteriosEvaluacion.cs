using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinariaApi.Models
{
    public class CriteriosEvaluacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? NombreCriterio { get; set; }
        public string? Descripcion { get; set; }
        public string? TipoCriterio { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }

        // Ejemplo: "Calificación", "Observación", etc.
    }
}
