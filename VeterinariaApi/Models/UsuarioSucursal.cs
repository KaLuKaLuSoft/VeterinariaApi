using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinariaApi.Models
{
    public class UsuarioSucursal
    {
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Login? Usuario { get; set; }

        public int SucursalId { get; set; }
        [ForeignKey("SucursalId")]
        public Sucursales? Sucursal { get; set; }
        public bool? Activo { get; set; } = false;
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
