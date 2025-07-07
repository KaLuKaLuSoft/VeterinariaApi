using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface ILoginAccionesRepositorio
    {
        Task<List<DtoLoginAcciones>> GetLoginAcciones();
        Task<DtoLoginAcciones> GetLoginAccionesById(int id);
        Task<DtoLoginAcciones> Create(DtoLoginAcciones loginaccionesDto);
        Task<DtoLoginAcciones> Update(DtoLoginAcciones loginaccionesDto);
        Task<bool> DeleteLoginAcciones(int id);
        Task<bool> LoginAccionesExists(int id);
    }
}
