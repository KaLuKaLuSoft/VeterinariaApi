using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface IDepartamentosRepositorio
    {
        Task<List<DtoDepartamentos>> GetDepartamentos();
        Task<DtoDepartamentos> GetDepartamentosById(int id);
        Task<DtoDepartamentos> Create(DtoDepartamentos departamentosDto);
        Task<DtoDepartamentos> Update(DtoDepartamentos departamentosDto);
        Task<bool> DeleteDepartamentos(int id);
        Task<bool> DeapartamentosExists(int id);
    }
}
