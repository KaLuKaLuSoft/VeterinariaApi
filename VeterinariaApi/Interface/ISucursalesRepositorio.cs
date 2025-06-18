using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface ISucursalesRepositorio
    {
        Task<List<DtoSucursales>> GetSucursales();
        Task<DtoSucursales> GetSucursalesById(int id);
        Task<DtoSucursales> Create(DtoSucursales sucursalesDto);
        Task<DtoSucursales> Update(DtoSucursales sucursalesDto);
        Task<bool> DeleteSucursales(int id);
        Task<bool> SucursalesExists(int id);
    }
}
