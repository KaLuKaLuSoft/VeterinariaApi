namespace VeterinariaApi.Dto
{
    public class DtoAcciones
    {
        public int Id { get; set; }
        public string? NombreAcciones { get; set; }
        public bool? Activo { get; set; } = false;
        public string? Descripcion { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
