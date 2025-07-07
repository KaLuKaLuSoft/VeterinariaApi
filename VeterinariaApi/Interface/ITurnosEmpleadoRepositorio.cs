using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface ITurnosEmpleadoRepositorio
    {
        Task<List<DtoTurnosEmpleado>> GetTurnosEmpleado();
        Task<DtoTurnosEmpleado> GetTurnosEmpleadoById(int id);
        Task<DtoTurnosEmpleado> Create(DtoTurnosEmpleado turnosEmpleadoDto);
        Task<DtoTurnosEmpleado> Update(DtoTurnosEmpleado turnosEmpleadoDto);
        Task<bool> DeleteTurnosEmpleado(int id);
        Task<bool> TurnosEmpleadoExists(int id);
    }
}
