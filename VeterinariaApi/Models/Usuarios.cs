using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace VeterinariaApi.Models
{
    public class Usuarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Contrasena { get; set; }
        public string? Email { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime UltimoLogin { get; set; }
        public int? IdSucursal { get; set; }
        [ForeignKey("IdSucursal")]
        public Sucursales? NombreSucursal { get; set; }
        public int? IdRol { get; set; }
        [ForeignKey("IdRol")]
        public Roles? NombreRol { get; set; }
        public int? IdEmpleado { get; set; }
        [ForeignKey("IdEmpleado")]
        public Empleados? NombreEmpleado { get; set; }
        public bool? Activo { get; set; }
        public string? Tokens { get; set; }
        public DateTime? Expiration { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
