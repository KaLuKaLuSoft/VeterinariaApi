using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface IClientesRepositorio
    {
        Task<List<DtoClientes>> GetClientes(int idEmpresa);
        Task<DtoClientes> GetClientesById(int id, int idEmpresa);
        Task<DtoClientes> Create(DtoClientes clientesDto);
        Task<DtoClientes> Update(DtoClientes clientesDto);
        Task<bool> DeleteClientes(int id, int idEmpresa);        // Se agregó idEmpresa
        Task<bool> ClientesExists(int id);        // Se agregó idEmpresa
    }
}
