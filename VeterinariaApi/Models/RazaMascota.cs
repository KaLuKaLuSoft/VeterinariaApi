using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinariaApi.Models
{
    public class RazaMascota
    {
        public int Id { get; set; }
        public string? NombreRaza { get; set; }
        public int IdEspecieMascota { get; set; }
        [ForeignKey("IdEspecieMascota")]
        public EspecieMascota? EspecieMascota { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
