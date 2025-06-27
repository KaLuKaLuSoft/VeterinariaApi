using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface ISubModuloRepositorio
    {
        Task<List<DtoSubModulo>> GetSubModulo();
        Task<DtoSubModulo> GetSubModuloById(int id);
        Task<DtoSubModulo> Create(DtoSubModulo submoduloDto);
        Task<DtoSubModulo> Update(DtoSubModulo submoduloDto);
        Task<bool> DeleteSubModulo(int id);
        Task<bool> SubModuloExists(int id);
    }
}
