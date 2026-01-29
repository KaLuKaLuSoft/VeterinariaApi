namespace VeterinariaApi.Dto
{
    public class DtoCriterioEvaluacion
    {
        public int Id { get; set; }
        public string? NombreCriterio { get; set; }
        public bool? Activo { get; set; } = false;

        public string? Descripcion { get; set; }
        public string? TipoCriterio { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
