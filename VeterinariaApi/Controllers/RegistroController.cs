using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;

namespace VeterinariaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly IRegistroRepositorio _registroRepo;
        public RegistroController(IRegistroRepositorio registroRepo, ApplicationDbContext context)
        {
            _registroRepo = registroRepo;
            _context = context;
        }

        [HttpPost]
        public async Task<object> Post([FromBody] DtoRegistro registroDto)
        {
            // Instanciamos tu ResponseDto global
            var response = new ResponseDto();

            try
            {
                // Llamamos al repositorio que ejecuta el SP
                await _registroRepo.Create(registroDto);

                response.IsSuccess = true;
                response.DisplayMessage = "El correo ya está registrado";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.DisplayMessage = ex.Message;
            }

            return response;
        }

        [HttpGet("verificar-email")]
        public async Task<bool> ExisteEmail(string email)
        {
            return await _context.Login.AnyAsync(x => x.Usuario == email);
        }
    }
}
