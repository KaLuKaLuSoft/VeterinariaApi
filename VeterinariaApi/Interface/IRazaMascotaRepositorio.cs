using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface IRazaMascotaRepositorio
    {
        Task<List<DtoRazaMascota>> GetRazaMascota();
        Task<DtoRazaMascota> GetRazaMascotaById(int id);
        Task<DtoRazaMascota> Create(DtoRazaMascota razaMascotaDto);
        Task<DtoRazaMascota> Update(DtoRazaMascota razaMascotaDto);
        Task<bool> DeleteRazaMascota(int id);
        Task<bool> RazaMascotaExists(int id);
    }
}
