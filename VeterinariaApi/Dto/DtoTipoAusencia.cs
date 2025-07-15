namespace VeterinariaApi.Dto
{
    public class DtoTipoAusencia
    {
        public int Id { get; set; }
        public string? NombreAusencia { get; set; }
        public bool RequiereAprobacion { get; set; } = false;
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
