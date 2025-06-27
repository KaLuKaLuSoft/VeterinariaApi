using System.ComponentModel.DataAnnotations.Schema;
using VeterinariaApi.Models;

namespace VeterinariaApi.Dto
{
    public class DtoLogin
    {
        public int Id { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Contrasena { get; set; }
        public string? Email { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime UltimoLogin { get; set; }
        public int? IdSucursal { get; set; }
        public Sucursales? NombreSucursal { get; set; }
        public int? IdRol { get; set; }
        public Roles? NombreRol { get; set; }
        public int? IdEmpleado { get; set; }
        public Empleados? NombreEmpleado { get; set; }
        public bool? Activo { get; set; }
        public string? Tokens { get; set; }
        public DateTime? Expiration { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
