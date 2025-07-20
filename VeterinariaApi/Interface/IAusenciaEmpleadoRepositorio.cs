using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface IAusenciaEmpleadoRepositorio
    {
        Task<List<DtoAusenciaEmpleado>> GetAusenciaEmpleado();
        Task<DtoAusenciaEmpleado> GetAusenciaEmpleadoById(int id);
        Task<DtoAusenciaEmpleado> Create(DtoAusenciaEmpleado ausenciaEmpleadoDto);
        Task<DtoAusenciaEmpleado> Update(DtoAusenciaEmpleado ausenciaEmpleadoDto);
        Task<bool> DeleteAusenciaEmpleado(int id);
        Task<bool> AusenciaEmpleadoExists(int id);
    }
}
