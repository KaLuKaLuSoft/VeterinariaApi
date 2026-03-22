using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface IEspecieMascotaRepositorio
    {
        Task<List<DtoEspecieMascota>> GetEspecieMascota();
        Task<DtoEspecieMascota> GetEspecieMascotaById(int id);
        Task<DtoEspecieMascota> Create(DtoEspecieMascota especieMascotaDto);
        Task<DtoEspecieMascota> Update(DtoEspecieMascota especieMascotaDto);
        Task<bool> DeleteEspecieMascota(int id);
        Task<bool> EspecieMascotaExists(int id);
    }
}
