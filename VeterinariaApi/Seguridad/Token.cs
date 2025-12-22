using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VeterinariaApi.Models;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using VeterinariaApi.Data;

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
        public (string jwtToken, string refreshToken, DateTime expiration) GenerarTokens(Login usuario)
        {
            var userClaims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Usuario!)
        };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var expiration = DateTime.UtcNow.AddMinutes(1); // Expiración del JWT

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
