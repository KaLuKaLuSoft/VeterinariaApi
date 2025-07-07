using VeterinariaApi.Dto;
using VeterinariaApi.Interface;

namespace VeterinariaApi.Repositorio
{
    public class TipoTurnoRepositorio : ITipoTurnoRepositorio
    {
        public Task<DtoTipoTurno> Create(DtoTipoTurno tipoturnoDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTipoTurno(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<DtoTipoTurno>> GetTipoTurno()
        {
            throw new NotImplementedException();
        }

        public Task<DtoTipoTurno> GetTipoTurnoById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TipoTurnoExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DtoTipoTurno> Update(DtoTipoTurno tipoturnoDto)
        {
            throw new NotImplementedException();
        }
    }
}
