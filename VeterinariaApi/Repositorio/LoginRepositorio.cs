using VeterinariaApi.Dto;
using VeterinariaApi.Interface;

namespace VeterinariaApi.Repositorio
{
    public class LoginRepositorio : ILoginRepositorio
    {
        public Task<DtoLogin> Create(DtoLogin loginDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteLogin(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<DtoLogin>> GetLogin()
        {
            throw new NotImplementedException();
        }

        public Task<DtoLogin> GetLoginById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LoginExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DtoLogin> Update(DtoLogin loginDto)
        {
            throw new NotImplementedException();
        }
    }
}
