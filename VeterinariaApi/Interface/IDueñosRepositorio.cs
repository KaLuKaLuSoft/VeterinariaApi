using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface IDueñosRepositorio
    {
        Task<List<DtoDueños>> GetDueños(int idEmpresa);
        Task<DtoDueños> GetDueñosById(int id, int idEmpresa);
        Task<DtoDueños> Create(DtoDueños dueñosDto);
        Task<DtoDueños> Update(DtoDueños dueñosDto);
        Task<bool> DeleteDueños(int id, int idEmpresa);
        Task<bool> DueñosExists(int id);
    }
}
