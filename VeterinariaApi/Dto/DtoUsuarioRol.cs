namespace VeterinariaApi.Dto
{
    public class DtoUsuarioRol
    {
        public int UsuarioId { get; set; }
        public int RolId { get; set; }
        public bool? Activo { get; set; } = false;
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }

    }
}
