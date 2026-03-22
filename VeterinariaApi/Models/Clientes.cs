using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinariaApi.Models
{
    public class Clientes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? CodCliente { get; set; }
        public string? NombreCliente { get; set; }
        public string? DireccionCliente { get; set; }
        public string? Email { get; set; }
        public int? Celular { get; set; }
        public bool? Activo { get; set; } = false;
        public bool? IsDeleted { get; set; } = false;
        public DateTime Fecha_Registro { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public string? Observaciones { get; set; }
        public int IdTipoCliente { get; set; }
        [ForeignKey("IdTipoCliente")]
        public TipoCliente? TipoCliente { get; set; }
        public int IdCiudad { get; set; }
        [ForeignKey("IdCiudad")]
        public Ciudad? Ciudad { get; set; }
        public int IdEmpresa { get; set; }
        [ForeignKey("IdEmpresa")]
        public Empresa? Empresa { get; set; }
    }
}
