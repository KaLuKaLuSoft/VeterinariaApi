using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;

namespace VeterinariaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroController : ControllerBase
    {
        private readonly IRegistroRepositorio _registroRepo;
        public RegistroController(IRegistroRepositorio registroRepo)
        {
            _registroRepo = registroRepo;
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
                response.DisplayMessage = "Se registró el usuario y la empresa correctamente";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.DisplayMessage = "Error al realizar el registro";
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }
    }
}
