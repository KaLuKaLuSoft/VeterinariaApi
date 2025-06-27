namespace VeterinariaApi.Dto
{
    public class DtoModulo
    {
        public int Id { get; set; }
        public string? NombreModulo { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
