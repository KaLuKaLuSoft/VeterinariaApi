using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace VeterinariaApi.Models
{
    public class Login
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Usuario { get; set; }
        public string? Contrasena { get; set; }

        //public int? IdRol { get; set; }
        //[ForeignKey("IdRol")]
        //public Roles? Roles { get; set; }
        public bool? Activo { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }

        public int? IdEmpleado { get; set; }
        [ForeignKey("IdEmpleado")]
        public Empleados? NombreEmpleado { get; set; }
        
        public string? Tokens { get; set; }
        public DateTime? Expiration { get; set; }
        public string? RefreshToken { get; set; }


        public DateTime FechaCreacion { get; set; }
        public DateTime? UltimoLogin { get; set; }
    }
}
