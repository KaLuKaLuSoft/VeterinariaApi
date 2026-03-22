using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinariaApi.Models
{
    public class Dueños
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? CodDueños { get; set; }
        public string? NumeroIdentificacion{ get; set; } 
        public string? NombreCompleto { get; set; }
        public int? Celular { get; set; }
        public string? CorreoElectronico { get; set; }
        public int IdCiudad { get; set; }
        [ForeignKey("IdCiudad")]
        public Ciudad? NombreCiudad { get; set; }
        public string? Direccion { get; set; }  
        public int IdTipoDocumento { get; set; }
        [ForeignKey("IdTipoDocumento")]
        public TipoDocumentos? TipoDocumento { get; set; }
        public int IdEmpresa { get; set; }
        [ForeignKey("IdEmpresa")]
        public Empresa? Empresa { get; set; }
        public bool? Activo { get; set; } = false;
        public bool? IsDeleted { get; set; } = false;
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }

    }
    //CE - Carné de Extranjera(Residencia Temporal o Permanente) ,
    //CI - Cédula de Identidad,
    //CIE - Cédula de Identidad para Extranjeros,
    //CR - Carné de Refugiado,
    //LSM - Libreta de Servicio Militar,
    //NIT -Número de Identificación Tributaria,
    //PAS - Pasaporte
}
