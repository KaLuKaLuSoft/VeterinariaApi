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
            // Hacer el cruce (JOIN) manual entre Login, Empleado y Empresa
            var query = from l in _context.Login
                        where l.RefreshToken == refreshToken
                        join e in _context.Empleados on l.IdEmpleado equals e.Id // Verifica "IdEmpleado"
                        join em in _context.Empresas on e.IdEmpresa equals em.Id // Verifica "IdEmpresa"
                        select new DtoLogin
                        {
                            Id = l.Id,
                            Usuario = l.Usuario,
                            Contrasena = l.Contrasena,

                            // Obtenidos del JOIN:
                            IdEmpresa = em.Id,
                            IdPais = em.IdPais
                        };
            var loginDto = await query.FirstOrDefaultAsync();
            return loginDto;
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
