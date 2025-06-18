using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinariaApi.Dto
{
    public class DtoRegiones
    {
        public int Id { get; set; }
        public string? NombreDepartamento { get; set; }
        public int IdPais { get; set; }
        public string? NombrePais { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
