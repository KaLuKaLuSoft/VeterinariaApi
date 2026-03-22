using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VeterinariaApi.Models;

namespace VeterinariaApi.Dto
{
    public class DtoDueños
    {
        public int Id { get; set; }
        public string? CodDueños { get; set; }
        public string? NumeroIdentificacion { get; set; }
        public string? NombreCompleto { get; set; }
        public int? Celular { get; set; }
        public string? CorreoElectronico { get; set; }
        public int IdCiudad { get; set; }
        public string? NombreCiudad { get; set; }
        public string? Direccion { get; set; }
        public int IdTipoDocumento { get; set; }
        public string? TipoDocumento { get; set; }
        public int IdEmpresa { get; set; }
        public string? Empresa { get; set; }
        public bool? Activo { get; set; } = false;
        public bool? IsDeleted { get; set; } = false;
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
