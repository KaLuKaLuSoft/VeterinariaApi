using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface IRegionesRepositorio
    {
        Task<List<DtoRegiones>> GetRegiones();
        Task<DtoRegiones> GetRegionesById(int id);
        Task<DtoRegiones> Create(DtoRegiones regionesDto);
        Task<DtoRegiones> Update(DtoRegiones regionesDto);
        Task<bool> DeleteRegiones(int id);
        Task<bool> RegionesExists(int id);
    }
}