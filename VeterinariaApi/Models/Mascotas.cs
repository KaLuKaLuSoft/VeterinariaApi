using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinariaApi.Models
{
    public class Mascotas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? NombreMascota { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? IdentificacionUnica { get; set; }
        public decimal? Peso { get; set; }
        public int IdEmpresa { get; set; }
        [ForeignKey("IdEmpresa")]
        public Empresa? Empresa { get; set; }
        public int IdEspecie { get; set; }
        [ForeignKey("IdEspecie")]
        public EspecieMascota? EspecieMascota { get; set; }
        public int IdRaza { get; set; }
        [ForeignKey("IdRaza")]
        public RazaMascota? RazaMascota { get; set; }
    }
}
