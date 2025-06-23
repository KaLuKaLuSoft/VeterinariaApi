namespace VeterinariaApi.Dto
{
    public class DtoDepartamentos
    {
        public int Id { get; set; }
        public string? NombreDepartamento { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
