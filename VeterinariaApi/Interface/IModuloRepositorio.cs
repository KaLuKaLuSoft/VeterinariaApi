using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface IModuloRepositorio
    {
        Task<List<DtoModulo>> GetModulo();
        Task<DtoModulo> GetModuloById(int id);
        Task<DtoModulo> Create(DtoModulo moduloDto);
        Task<DtoModulo> Update(DtoModulo moduloDto);
        Task<bool> DeleteModulo(int id);
        Task<bool> ModuloExists(int id);
    }
}
