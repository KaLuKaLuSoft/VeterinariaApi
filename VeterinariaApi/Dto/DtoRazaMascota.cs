using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinariaApi.Dto
{
    public class DtoRazaMascota
    {
        public int Id { get; set; }
        public string? NombreRaza { get; set; }
        public int IdEspecieMascota { get; set; }
        public string? EspecieMascota { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
