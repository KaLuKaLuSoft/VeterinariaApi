using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface IUsuarioSucursalRepositorio
    {
        Task<List<DtoUsuarioSucursal>> GetUsuarioSucursal();
        Task<DtoUsuarioSucursal> GetUsuarioSucursalById(int usuarioId, int sucursalId);
        Task<DtoUsuarioSucursal> Create(DtoUsuarioSucursal usuarioSucursalDto);
        Task<DtoUsuarioSucursal> Update(DtoUsuarioSucursal usuarioSucursalDto);
        Task<bool> DeleteUsuarioSucursal(int usuarioId, int sucursalId);
        Task<bool> UsuarioSucursalExists(int usuarioId, int sucursalId);
    }
}
