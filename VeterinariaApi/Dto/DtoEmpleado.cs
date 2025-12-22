using System.ComponentModel.DataAnnotations.Schema;
using VeterinariaApi.Models;

namespace VeterinariaApi.Dto
{
    public class DtoEmpleado
    {
        public int Id { get; set; }
        public string? CodEmpleado { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Direccion { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Celular { get; set; }
        public string? Ci { get; set; }
        public DateTime? FechaContratacion { get; set; }
        public int? IdSucursal { get; set; }
        //public int CajaId { get; set; }
        //[ForeignKey("CajaId")]
        //public Caja Caja { get; set; }
        public decimal? Salario { get; set; }
        public int? IdDepartamento { get; set; }
        public string? NombreDepartamento { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
