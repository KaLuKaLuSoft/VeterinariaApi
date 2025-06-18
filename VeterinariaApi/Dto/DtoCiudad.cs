namespace VeterinariaApi.Dto
{
    public class DtoCiudad
    {
        public int Id { get; set; }
        public string? NombreCiudad { get; set; }
        public int IdRegion { get; set; }
        public string? NombreRegion { get; set; } // Assuming you want to include the region name
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set;}
    }
}
