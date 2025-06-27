namespace VeterinariaApi.Models
{
    public class SubModulo
    {
        public int Id { get; set; }
        public string? NombreSubModulo { get; set; }
        public string? Descripcion { get; set; }
        public int ModuloId { get; set; }
        public Modulo? NombreModulo { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
