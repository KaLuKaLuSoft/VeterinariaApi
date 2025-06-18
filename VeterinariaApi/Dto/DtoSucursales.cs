using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VeterinariaApi.Models;

namespace VeterinariaApi.Dto
{
    public class DtoSucursales
    {
        public int Id { get; set; }
        public string? NombreSucursal { get; set; }
        public string? Direccion { get; set; }
        public int? IdCiudad { get; set; }
        public string? NombreCiudad { get; set; }
        public string? Telefono { get; set; }
        public string? EmailContacto { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
