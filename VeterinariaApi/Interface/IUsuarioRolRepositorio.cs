using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface IUsuarioRolRepositorio
    {
        Task<List<DtoUsuarioRol>> GetUsuarioRol();
        Task<DtoUsuarioRol> GetUsuarioRolById(int usuarioId, int rolId);
        Task<DtoUsuarioRol> Create(DtoUsuarioRol usuarioRolDto);
        Task<DtoUsuarioRol> Update(DtoUsuarioRol usuarioRolDto);
        Task<bool> DeleteUsuarioRol(int usuarioId, int rolId);
        Task<bool> UsuarioRolExists(int usuarioId, int rolId);
    }
}
