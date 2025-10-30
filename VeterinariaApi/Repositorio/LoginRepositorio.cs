using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;

namespace VeterinariaApi.Repositorio
{
    public class LoginRepositorio : ILoginRepositorio
    {
        private readonly ApplicationDbContext? _context;
        private IMapper? _mapper;

        public LoginRepositorio(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

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

        public async Task<DtoLogin> GetLoginByRefreshToken(string refreshToken)
        {
            var loginEntity = await _context.Login
                .FirstOrDefaultAsync(l => l.RefreshToken == refreshToken);
            if (loginEntity == null) return null;

            return new DtoLogin
            {
                Id = loginEntity.Id,
                NombreUsuario = loginEntity.NombreUsuario,
                Contrasena = loginEntity.Contrasena
            };
        }

        public Task<bool> LoginExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DtoLogin> Update(DtoLogin loginDto)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateLogin(int loginId, RefreshTokens refreshTokens)
        {
            var loginEntity = await _context.Login.FindAsync(loginId);
            if (loginEntity != null)
            {
                loginEntity.Tokens = refreshTokens.Tokens;
                loginEntity.Expiration = refreshTokens.Expiration;
                loginEntity.RefreshToken = refreshTokens.RefreshToken;
                loginEntity.UltimoLogin = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
    }
}
