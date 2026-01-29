namespace VeterinariaApi.Dto
{
    public class DtoConceptoNominas
    {
        public int Id { get; set; }
        public string? NombreNomina { get; set; }
        public string? TipoConcepto { get; set; }
        public bool? EsFijo { get; set; }
        public bool? Activo { get; set; } = false;
        public string? Descripcion { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
