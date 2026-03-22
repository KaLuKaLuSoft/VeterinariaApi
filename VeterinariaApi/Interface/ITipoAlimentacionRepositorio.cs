using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface ITipoAlimentacionRepositorio
    {
        Task<List<DtoTipoAlimentacion>> GetTipoAlimentacion();
        Task<DtoTipoAlimentacion> GetTipoAlimentacionById(int id);
        Task<DtoTipoAlimentacion> Create(DtoTipoAlimentacion tipoAlimentacionDto);
        Task<DtoTipoAlimentacion> Update(DtoTipoAlimentacion tipoAlimentacionDto);
        Task<bool> DeleteTipoAlimentacion(int id);
        Task<bool> TipoAlimentacionExists(int id);
    }
}
