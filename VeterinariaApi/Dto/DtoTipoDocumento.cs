namespace VeterinariaApi.Dto
{
    public class DtoTipoDocumento
    {
        public int Id { get; set; }
        public string? TipoDocumento { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
