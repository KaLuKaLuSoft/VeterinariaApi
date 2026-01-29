namespace VeterinariaApi.Dto
{
    public class DtoEpecialidadesMedicas
    {
        public int Id { get; set; }
        public string? NombreEspecialidad { get; set; }
        public string? Descripcion { get; set; }
        public bool? Activo { get; set; } = false;
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
