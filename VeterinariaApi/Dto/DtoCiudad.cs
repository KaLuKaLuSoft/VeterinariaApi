namespace VeterinariaApi.Dto
{
    public class DtoCiudad
    {
        public int Id { get; set; }
        public string? NombreCiudad { get; set; }
        public int IdPais { get; set; }
        public string? NombrePaises { get; set; } // Assuming you want to include the region name
        public bool? Activo { get; set; } = false;
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set;}
    }
}
