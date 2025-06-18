using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface ICiudadRepositorio
    {
        Task<List<DtoCiudad>> GetCiudad();
        Task<DtoCiudad> GetCiudadById(int id);
        Task<DtoCiudad> Create(DtoCiudad ciudadDto);
        Task<DtoCiudad> Update(DtoCiudad ciudadDto);
        Task<bool> DeleteCiudad(int id);
        Task<bool> CiudadExists(int id);
    }
}
