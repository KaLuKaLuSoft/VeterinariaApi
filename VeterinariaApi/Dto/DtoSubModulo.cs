using VeterinariaApi.Models;

namespace VeterinariaApi.Dto
{
    public class DtoSubModulo
    {
        public int Id { get; set; }
        public string? NombreSubModulo { get; set; }
        public string? Descripcion { get; set; }
        public int ModuloId { get; set; }
        public string? NombreModulo { get; set; }
        public bool? Activo { get; set; } = false;
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
