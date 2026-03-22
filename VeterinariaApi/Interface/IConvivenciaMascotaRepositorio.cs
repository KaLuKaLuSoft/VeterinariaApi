using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface IConvivenciaMascotaRepositorio
    {
        Task<List<DtoConvivenciaMascota>> GetConvivenciaMascota();
        Task<DtoConvivenciaMascota> GetConvivenciaMascotaById(int id);
        Task<DtoConvivenciaMascota> Create(DtoConvivenciaMascota convivenciaMascotaDto);
        Task<DtoConvivenciaMascota> Update(DtoConvivenciaMascota convivenciaMascotaDto);
        Task<bool> DeleteConvivenciaMascota(int id);
        Task<bool> ConvivenciaMascotaExists(int id);
    }
}
