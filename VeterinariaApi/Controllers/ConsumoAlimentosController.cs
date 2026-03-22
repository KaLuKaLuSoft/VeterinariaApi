using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;
using VeterinariaApi.Models;

namespace VeterinariaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumoAlimentosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConsumoAlimentoRepositorio _consumoAlimentoRepositorio;
        private readonly ILogger<ConsumoAlimentosController> _logger;
        private readonly ResponseDto _response;

        public ConsumoAlimentosController(ApplicationDbContext context, ILogger<ConsumoAlimentosController> logger, IConsumoAlimentoRepositorio consumoAlimentoRepositorio)
        {
            _context = context;
            _consumoAlimentoRepositorio = consumoAlimentoRepositorio;
            _logger = logger;
            _response = new ResponseDto();
        }

        // GET: api/ConsumoAlimentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsumoAlimento>>> GetConsumoAlimento()
        {
            try
            {
                var consumoalimentos = await _consumoAlimentoRepositorio.GetConsumoAlimento();
                if(consumoalimentos == null || !consumoalimentos.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron registros de consumo de alimentos.";
                    return NotFound(_response);
                }
                return Ok(consumoalimentos);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Ocurrió un error al obtener los registros de consumo de alimentos.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener todo el consumo de alimento. ", Details = ex.Message});
            }
        }

        // GET: api/ConsumoAlimentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsumoAlimento>> GetConsumoAlimento(int id)
        {
            if(!await _consumoAlimentoRepositorio.ConsumoAlimentoExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "No se encontró el registro de consumo de alimento con el ID proporcionado.";
                return Ok(_response);
            }
            try
            {
                var consumoalimento = await _consumoAlimentoRepositorio.GetConsumoAlimentoById(id);
                if(consumoalimento != null)
                {
                    _response.Result = consumoalimento;
                    _response.DisplayMessage = "Se encontró el registro de consumo de alimento. ";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontró el registro de consumo de alimento. ";
                    return NotFound(_response);
                }

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Ocurrió un error al obtener el registro de consumo de alimento. ");
                return StatusCode(500, _response);
            }
        }

        // PUT: api/ConsumoAlimentos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsumoAlimento(int id, DtoConsumoAlimento consumoAlimentoDto)
        {
            if(!await _consumoAlimentoRepositorio.ConsumoAlimentoExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "No se encontró el registro de consumo de alimento con el ID proporcionado.";
                return NotFound(_response);
            }
            try
            {
                var consumo = await _consumoAlimentoRepositorio.Update(consumoAlimentoDto);
                _response.Result = consumo;
                _response.DisplayMessage = "Se actualizó el registro de consumo de alimento. ";
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.DisplayMessage = "Ocurrió un error al actualizar el registro de consumo de alimento. ";
                return BadRequest(_response);
            }
        }

        // POST: api/ConsumoAlimentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ConsumoAlimento>> PostConsumoAlimento(DtoConsumoAlimento consumoAlimentoDto)
        {
            try
            {
                DtoConsumoAlimento consumo = await _consumoAlimentoRepositorio.Create(consumoAlimentoDto);
                return StatusCode(201, new { Message = "Registro de consumo de alimento creado exitosamente. ", Data = consumo });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al crear el registro de consumo de alimento. ");
                return BadRequest(new { Message = "Error al crear el registro de consumo de alimento. ", Details = ex.Message });
            }
        }

        // DELETE: api/ConsumoAlimentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsumoAlimento(int id)
        {
            try
            {
                bool deleted = await _consumoAlimentoRepositorio.DeleteConsumoAlimento(id);
                if (deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "No se encontró el registro de consumo de alimento. " });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el consumo de alimento. ");
                return StatusCode(500, new { Message = "Error al eliminar el registro de consumo de alimento. ", Details = ex.Message });
            }
        }

        private bool ConsumoAlimentoExists(int id)
        {
            return _context.ConsumoAlimento.Any(e => e.Id == id);
        }
    }
}
