using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface ILogueoRepositorio
    {
        Task<DtoLogin?> AuthenticateUser(string usuario, string password);
    }
}
