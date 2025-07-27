using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface IEvaluacionEmpleadoRepositorio
    {
        Task<List<DtoEvaluacionEmpleado>> GetEvaluacionEmpleado();
        Task<DtoEvaluacionEmpleado> GetEvaluacionEmpleadoById(int id);
        Task<DtoEvaluacionEmpleado> Create(DtoEvaluacionEmpleado evaluacionEmpleadoDto);
        Task<DtoEvaluacionEmpleado> Update(DtoEvaluacionEmpleado evaluacionEmpleadoDto);
        Task<bool> DeleteEvaluacionEmpleado(int id);
        Task<bool> EvaluacionEmpleadoExists(int id);
    }
}
