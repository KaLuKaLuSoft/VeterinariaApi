namespace VeterinariaApi.Dto
{
    public class DtoCategoriaActivoFijo
    {
        public int Id { get; set; }
        public string? NombreCategoriaActivoFijo { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
