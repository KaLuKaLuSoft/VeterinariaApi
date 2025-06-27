using VeterinariaApi.Dto;
using VeterinariaApi.Interface;

namespace VeterinariaApi.Repositorio
{
    public class SubModuloRepositorio : ISubModuloRepositorio
    {
        public Task<DtoSubModulo> Create(DtoSubModulo submoduloDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteSubModulo(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<DtoSubModulo>> GetSubModulo()
        {
            throw new NotImplementedException();
        }

        public Task<DtoSubModulo> GetSubModuloById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SubModuloExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DtoSubModulo> Update(DtoSubModulo submoduloDto)
        {
            throw new NotImplementedException();
        }
    }
}
