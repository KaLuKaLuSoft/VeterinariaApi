using System.ComponentModel.DataAnnotations.Schema;
using VeterinariaApi.Models;

namespace VeterinariaApi.Dto
{
    public class DtoLogin
    {
        public int Id { get; set; }
        public string? Usuario { get; set; }
        public string? Empleado { get; set; }
        public string? Contrasena { get; set; }
        public int? IdRol { get; set; }
        public string? Roles { get; set; }
        public bool? Activo { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public int? IdEmpleado { get; set; }

        public DtoSucursales Sucursal { get; set; }
        public List<DtoLoginAcciones> Reglas { get; set; }
        public List<DtoLoginMenu> Menus { get; set; }
        
        public DateTime FechaCreacion { get; set; }
        public DateTime? UltimoLogin { get; set; }

        public string? Tokens { get; set; }

        public int? IdEmpresa { get; set; }
        public string? Empresa { get; set; }
    }
    public class RefreshTokens
    {
        public string? Tokens { get; set; }
        public DateTime? Expiration { get; set; }
        public string RefreshToken { get; set; }
    }
    public class RegLoginDto
    {
        public int Id { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Contrasena { get; set; }
        public int? IdRol { get; set; }
        public int? IdEmpleado { get; set; }
        public bool? Activo { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
