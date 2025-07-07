using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface ITipoTurnoRepositorio
    {
        Task<List<DtoTipoTurno>> GetTipoTurno();
        Task<DtoTipoTurno> GetTipoTurnoById(int id);
        Task<DtoTipoTurno> Create(DtoTipoTurno tipoturnoDto);
        Task<DtoTipoTurno> Update(DtoTipoTurno tipoturnoDto);
        Task<bool> DeleteTipoTurno(int id);
        Task<bool> TipoTurnoExists(int id);
    }
}
