namespace VeterinariaApi.Dto
{
    public class DtoRoles
    {
        public int Id { get; set; }
        public string? NombreRol { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
