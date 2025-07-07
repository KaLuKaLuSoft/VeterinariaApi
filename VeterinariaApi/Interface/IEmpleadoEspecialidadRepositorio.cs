using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface IEmpleadoEspecialidadRepositorio
    {
        Task<List<DtoEmpleadoEspecialidad>> GetEmpleadoEspecialidad();
        Task<DtoEmpleadoEspecialidad> GetEmpleadoEspecialidadById(int empleadoId, int especialidadId);
        Task<DtoEmpleadoEspecialidad> Create(DtoEmpleadoEspecialidad empleadoespecialidadDto);
        Task<DtoEmpleadoEspecialidad> Update(DtoEmpleadoEspecialidad empleadoespecialidadDto);
        Task<bool> DeleteEmpleadoEspecialidad(int empleadoId, int especialidadId);
        Task<bool> EmpleadoEspecialidadExists(int empleadoId, int especialidadId);
    }
}
