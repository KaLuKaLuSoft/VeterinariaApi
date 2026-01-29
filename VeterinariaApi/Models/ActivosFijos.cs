using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinariaApi.Models
{
    public class ActivosFijos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? NombreActivo { get; set; }
        public string? NumeroSerie { get; set; }
        public int CategoriaActivoId { get; set; }
        [ForeignKey("CategoriaActivoId")]
        public CategoriaActivoFijo? CategoriaActivoFijo { get; set; }
        public int SucursalId { get; set; }
        [ForeignKey("SucursalId")]
        public Sucursales? Sucursal { get; set; }
        public DateTime? FechaAdquisicion { get; set; }
        public decimal? CostoAdquisicion { get; set; }
        public int? VidaUtil { get; set; }
        public string? UbicacionFisica { get; set; }
        public string? Estado { get; set; } // Activo, Inactivo, En Mantenimiento, Retirado
        public bool? Activo { get; set; } = false;
        public string? Observaciones { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
    