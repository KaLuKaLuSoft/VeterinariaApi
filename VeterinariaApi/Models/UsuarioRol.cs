using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinariaApi.Models
{
    public class UsuarioRol
    {
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Login? Usuario { get; set; }

        public int RolId { get; set; }
        [ForeignKey("RolId")]
        public Roles? Roles { get; set; }

        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
