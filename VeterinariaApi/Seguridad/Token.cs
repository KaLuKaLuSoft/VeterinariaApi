using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VeterinariaApi.Models;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;

namespace VeterinariaApi.Seguridad
{
    public class Token
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext? _context;

        public Token(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // Generar JWT y Refresh Token
        public (string jwtToken, string refreshToken, DateTime expiration) GenerarTokens(DtoLogin usuario)
        {
            // Construir claims de forma segura
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
            };

            // Agregar nombre si existe
            if (!string.IsNullOrWhiteSpace(usuario.Usuario))
            {
                claims.Add(new Claim(ClaimTypes.Name, usuario.Usuario));
            }

            // Agregar IdEmpresa si tiene valor
            if (usuario.IdEmpresa.HasValue)
            {
                claims.Add(new Claim("IdEmpresa", usuario.IdEmpresa.Value.ToString()));
            }

            var userClaims = claims.ToArray();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var expiration = DateTime.UtcNow.AddMinutes(60); // Expiración del JWT

            var jwtToken = new JwtSecurityToken(
                claims: userClaims,
                expires: expiration,
                signingCredentials: credentials
            );

            // Generar refresh token (puede ser una cadena aleatoria)
            var refreshToken = Guid.NewGuid().ToString();

            return (new JwtSecurityTokenHandler().WriteToken(jwtToken), refreshToken, expiration);
        }
    }
}
