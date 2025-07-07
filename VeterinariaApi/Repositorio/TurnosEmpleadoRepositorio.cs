using VeterinariaApi.Dto;
using VeterinariaApi.Interface;

namespace VeterinariaApi.Repositorio
{
    public class TurnosEmpleadoRepositorio : ITurnosEmpleadoRepositorio
    {
        public Task<DtoTurnosEmpleado> Create(DtoTurnosEmpleado turnosEmpleadoDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTurnosEmpleado(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<DtoTurnosEmpleado>> GetTurnosEmpleado()
        {
            throw new NotImplementedException();
        }

        public Task<DtoTurnosEmpleado> GetTurnosEmpleadoById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TurnosEmpleadoExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DtoTurnosEmpleado> Update(DtoTurnosEmpleado turnosEmpleadoDto)
        {
            throw new NotImplementedException();
        }
    }
}
