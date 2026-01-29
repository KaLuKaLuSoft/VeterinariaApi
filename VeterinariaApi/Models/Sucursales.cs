using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinariaApi.Models
{
    public class Sucursales
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? NombreSucursal { get; set; }
        public string? Direccion { get; set; }
        public int? IdCiudad { get; set; }
        [ForeignKey("IdCiudad")]
        public Ciudad? NombreCiudad { get; set; }
        public string? Telefono { get; set; }
        public string? EmailContacto { get; set; }
        public bool? Activo { get; set; } = false;
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
