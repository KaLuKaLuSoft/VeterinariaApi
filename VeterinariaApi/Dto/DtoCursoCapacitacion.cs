namespace VeterinariaApi.Dto
{
    public class DtoCursoCapacitacion
    {
        public int Id { get; set; }
        public string? NombreCurso { get; set; }
        public string? Descripcion { get; set; }
        public string? Duracion { get; set; }
        public string? Proveedor { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
