using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface ICriterioEvaluacionRepositorio
    {
        Task<List<DtoCriterioEvaluacion>> GetCriterioEvaluacion();
        Task<DtoCriterioEvaluacion> GetCriterioEvaluacionById(int id);
        Task<DtoCriterioEvaluacion> Create(DtoCriterioEvaluacion criterioEvaluacionDto);
        Task<DtoCriterioEvaluacion> Update(DtoCriterioEvaluacion criterioEvaluacionDto);
        Task<bool> DeleteCriterioEvaluacion(int id);
        Task<bool> CriterioEvaluacionExists(int id);
    }
}
