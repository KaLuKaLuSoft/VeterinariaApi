using VeterinariaApi.Dto;

namespace VeterinariaApi.Interface
{
    public interface ILoginRepositorio
    {
        Task<List<DtoLogin>> GetLogin();
        Task<DtoLogin> GetLoginById(int id);
        Task<DtoLogin> Create(DtoLogin loginDto);
        Task<DtoLogin> Update(DtoLogin loginDto);
        Task<bool> DeleteLogin(int id);
        Task<bool> LoginExists(int id);

        Task UpdateLogin(int loginId, RefreshTokens refreshTokens);
        Task<DtoLogin> GetLoginByRefreshToken(string refreshToken);
    }
}
