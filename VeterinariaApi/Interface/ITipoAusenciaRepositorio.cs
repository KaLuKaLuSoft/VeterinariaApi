using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface ITipoAusenciaRepositorio
    {
        Task<List<DtoTipoAusencia>> GetTipoAusencia();
        Task<DtoTipoAusencia> GetTipoAusenciaById(int id);
        Task<DtoTipoAusencia> Create(DtoTipoAusencia tipoAusenciaDto);
        Task<DtoTipoAusencia> Update(DtoTipoAusencia tipoAusenciaDto);
        Task<bool> DeleteTipoAusencia(int id);
        Task<bool> TipoAusenciaExists(int id);
    }
}
