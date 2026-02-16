using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface ITipoClientesRepositorio
    {
        Task<List<DtoTipoCliente>> GetTipoClientes();
        Task<DtoTipoCliente> GetTipoClientesById(int id);
        Task<DtoTipoCliente> Create(DtoTipoCliente tipoclientesDto);
        Task<DtoTipoCliente> Update(DtoTipoCliente tipoclientesDto);
        Task<DtoTipoCliente> Delete(DtoTipoCliente tipoclientesDto);
        Task<bool> DeleteTipoClientes(int id);
        Task<bool> TipoClientesExists(int id);
    }
}
