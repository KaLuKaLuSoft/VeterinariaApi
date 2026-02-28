using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;
using VeterinariaApi.Models;
using VeterinariaApi.Seguridad;

namespace VeterinariaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILoginRepositorio _loginRepositorio;
        private readonly ILogueoRepositorio _logueoRepositorio;
        private readonly ILogger<LoginController> _logger;
        protected ResponseDto _response;
        private readonly Token _tokenService;

        public LoginController(ApplicationDbContext context, ILoginRepositorio loginRepositorio,
            ILogger<LoginController> logger, ILogueoRepositorio logueoRepositorio, Token tokenService)
        {
            _context = context;
            _loginRepositorio = loginRepositorio;
            _logger = logger;
            _response = new ResponseDto();
            _logueoRepositorio = logueoRepositorio;

            _tokenService = tokenService;
        }

        // GET: api/Login
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Login>>> GetLogin()
        {
            return await _context.Login.ToListAsync();
        }

        // GET: api/Login/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Login>> GetLogin(int id)
        {
            var login = await _context.Login.FindAsync(id);

            if (login == null)
            {
                return NotFound();
            }

            return login;
        }

        // PUT: api/Login/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogin(int id, Login login)
        {
            if (id != login.Id)
            {
                return BadRequest();
            }

            _context.Entry(login).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Login
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Login>> PostLogin(Login login)
        {
            _context.Login.Add(login);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLogin", new { id = login.Id }, login);
        }

        // DELETE: api/Login/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogin(int id)
        {
            var login = await _context.Login.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }

            _context.Login.Remove(login);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoginExists(int id)
        {
            return _context.Login.Any(e => e.Id == id);
        }

        // Método para autenticar el usuario
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] DtoLogueo loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Usuario) || string.IsNullOrEmpty(loginRequest.Contrasena))
            {
                return BadRequest("Usuario o contraseña no pueden estar vacíos.");
            }

            try
            {
                var loginDto = await _logueoRepositorio.AuthenticateUser(loginRequest.Usuario, loginRequest.Contrasena);

                if (loginDto == null)
                {
                    return Unauthorized(new { Message = "Usuario o contraseña incorrectos." });
                }

                var (jwtToken, refreshToken, expiration) = _tokenService.GenerarTokens(loginDto);

                var refreshTokens = new RefreshTokens
                {
                    Tokens = jwtToken,
                    Expiration = DateTime.Now.AddMinutes(1),
                    RefreshToken = refreshToken
                };

                await _loginRepositorio.UpdateLogin(loginDto.Id, refreshTokens);

                var response = new
                {
                    Message = "Logueado con éxito",
                    Token = jwtToken,
                    RefreshToken = refreshToken,
                    Expiration = DateTime.Now.AddMinutes(1),
                    UserData = loginDto
                };

                return Ok(response);
            }
            catch (VeterinariaApi.Seguridad.UserBlockedException)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { Message = "Usuario bloqueado, póngase en contacto con el administrador." });
            }
            catch (Exception ex)
            {
                // Si el repositorio envuelve el error con el mensaje "Error al ingresar",
                // devolvemos 401 con un objeto JSON consistente para que el frontend pueda mostrarlo.
                if ((ex.Message != null && ex.Message.Contains("Error al ingresar")) ||
                    (ex.InnerException != null && ex.InnerException.Message != null && ex.InnerException.Message.Contains("Error al ingresar")))
                {
                    return Unauthorized(new { Message = "Usuario o contraseña incorrectos." });
                }

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al autenticar: {ex.Message}");
            }
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokens refreshTokenRequest)
        {
            if (string.IsNullOrEmpty(refreshTokenRequest.RefreshToken))
            {
                return BadRequest("El refresh token es requerido.");
            }

            try
            {
                var loginDto = await _loginRepositorio.GetLoginByRefreshToken(refreshTokenRequest.RefreshToken);
                if (loginDto == null)
                {
                    return Unauthorized("Refresh token inválido.");
                }

                var (newJwtToken, newRefreshToken, newExpiration) = _tokenService.GenerarTokens(loginDto);
                //var (newJwtToken, newRefreshToken, newExpiration) = _tokenService.GenerarTokens(new Login
                //{
                //    Id = loginDto.Id,
                //    Usuario = loginDto.Usuario,
                //    Contrasena = loginDto.Contrasena
                //});

                var newRefreshTokens = new RefreshTokens
                {
                    Tokens = newJwtToken,
                    Expiration = DateTime.Now.AddMinutes(10),
                    RefreshToken = newRefreshToken
                };

                await _loginRepositorio.UpdateLogin(loginDto.Id, newRefreshTokens);

                return Ok(new
                {
                    Tokens = newJwtToken,
                    RefreshToken = newRefreshToken
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al refrescar el token: {ex.Message}");
            }
        }
    }
}
