using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface IActivosFijosRepositorio
    {
        Task<List<DtoActivoFijos>> GetActivosFijos();
        Task<DtoActivoFijos> GetActivosFijosById(int id);
        Task<DtoActivoFijos> Create(DtoActivoFijos activosFijosDto);
        Task<DtoActivoFijos> Update(DtoActivoFijos activosFijosDto);
        Task<bool> DeleteActivosFijos(int id);
        Task<bool> ActivosFijosExists(int id);
    }
}
