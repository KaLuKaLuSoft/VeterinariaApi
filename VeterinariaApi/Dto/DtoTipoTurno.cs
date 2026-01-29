namespace VeterinariaApi.Dto
{
    public class DtoTipoTurno
    {
        public int Id { get; set; }
        public string? NombreTurno { get; set; }
        public bool? Activo { get; set; } = false;
        public TimeSpan? HoraInicio { get; set; }
        public TimeSpan? HoraFin { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
