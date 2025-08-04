using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface ICursoCapacitacionRepositorio
    {
        Task<List<DtoCursoCapacitacion>> GetCursoCapacitacion();
        Task<DtoCursoCapacitacion> GetCursoCapacitacionById(int id);
        Task<DtoCursoCapacitacion> Create(DtoCursoCapacitacion cursoCapacitacionDto);
        Task<DtoCursoCapacitacion> Update(DtoCursoCapacitacion cursoCapacitacionDto);
        Task<bool> DeleteCursoCapacitacion(int id);
        Task<bool> CursoCapacitacionExists(int id);
    }
}
