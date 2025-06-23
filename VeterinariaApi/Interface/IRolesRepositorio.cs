using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface IRolesRepositorio
    {
        Task<List<DtoRoles>> GetRoles();
        Task<DtoRoles> GetRolesById(int id);
        Task<DtoRoles> Create(DtoRoles rolesDto);
        Task<DtoRoles> Update(DtoRoles rolesDto);
        Task<bool> DeleteRoles(int id);
        Task<bool> RolesExists(int id);
    }
}
