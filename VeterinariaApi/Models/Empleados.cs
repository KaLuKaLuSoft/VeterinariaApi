using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinariaApi.Models
{
    public class Empleados
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? CodEmpleado { get; set; }
        public string? Empleado { get; set; }
        public string? Direccion { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Celular { get; set; }
        public string? Ci { get; set; }
        public DateTime? FechaContratacion { get; set; }
        public decimal? Salario { get; set; }
        public int? IdDepartamento { get; set; }
        [ForeignKey("IdDepartamento")]
        public Departamentos? NombreDepartamento { get; set; }
        public int? IdSucursal { get; set; }
        [ForeignKey("IdSucursal")]
        public Sucursales? Sucursal { get; set; }
        public int? IdEmpresa { get; set; }
        [ForeignKey("IdEmpresa")]
        public Empresa? NombreEmpresa { get; set; }
        //public int CajaId { get; set; }
        //[ForeignKey("CajaId")]
        //public Caja Caja { get; set; }
        public bool? Activo { get; set; } = false;
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }

    }
}