using System.ComponentModel.DataAnnotations.Schema;
using VeterinariaApi.Models;

namespace VeterinariaApi.Dto
{
    public class DtoClientes
    {
        public int Id { get; set; }
        public string? CodCliente { get; set; }
        public string? NombreCliente { get; set; }
        public string? DireccionCliente { get; set; }
        public string? Email { get; set; }
        public int? Celular { get; set; }
        public DateTime Fecha_Registro { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public string? Observaciones { get; set; }
        public int IdTipoCliente { get; set; }
        public string? TipoCliente { get; set; }
        public int IdCiudad { get; set; }
        public string? Ciudad { get; set; }
        public int IdEmpresa { get; set; }
        public string? Empresa { get; set; }
        public bool? Activo { get; set; } = false;
        public bool? IsDeleted { get; set; } = false;
    }
}
