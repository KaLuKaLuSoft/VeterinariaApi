namespace VeterinariaApi.Dto
{
    public class DtoEspecieMascota
    {
        public int Id { get; set; }
        public string? NombreEspecie { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
