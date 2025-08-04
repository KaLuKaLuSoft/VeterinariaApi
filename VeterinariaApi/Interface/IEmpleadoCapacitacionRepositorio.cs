using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface IEmpleadoCapacitacionRepositorio
    {
        Task<List<DtoEmpleadoCapacitacion>> GetEmpleadoCapacitacion();
        Task<DtoEmpleadoCapacitacion> GetEmpleadoCapacitacionById(int id);
        Task<DtoEmpleadoCapacitacion> Create(DtoEmpleadoCapacitacion empleadoCapacitacionDto);
        Task<DtoEmpleadoCapacitacion> Update(DtoEmpleadoCapacitacion empleadoCapacitacionDto);
        Task<bool> DeleteEmpleadoCapacitacion(int id);
        Task<bool> EmpleadoCapacitacionExists(int id);
    }
}
