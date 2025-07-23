namespace VeterinariaApi.Dto
{
    public class DtoUsuarioSucursal
    {
        public int UsuarioId { get; set; }
        public int SucursalId { get; set; }
        public DateTime? Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
