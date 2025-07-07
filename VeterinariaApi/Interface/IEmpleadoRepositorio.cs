using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface IEmpleadoRepositorio
    {
        Task<List<DtoEmpleado>> GetEmpleados();
        Task<DtoEmpleado> GetEmpleadoById(int id);
        Task<DtoEmpleado> Create(DtoEmpleado empleadoDto);
        Task<DtoEmpleado> Update(DtoEmpleado empleadoDto);
        Task<bool> DeleteEmpleado(int id);
        Task<bool> EmpleadoExists(int id);
    }
}
