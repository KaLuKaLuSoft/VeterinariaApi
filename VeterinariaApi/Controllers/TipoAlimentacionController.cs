using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;
using VeterinariaApi.Models;

namespace VeterinariaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoAlimentacionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITipoAlimentacionRepositorio _tipoAlimentacionRepositorio;
        private readonly ILogger<TipoAlimentacionController> _logger;
        protected ResponseDto _response;
        public TipoAlimentacionController(ApplicationDbContext context, ILogger<TipoAlimentacionController> logger, ITipoAlimentacionRepositorio tipoAlimentacionRepositorio)
        {
            _tipoAlimentacionRepositorio = tipoAlimentacionRepositorio;
            _logger = logger;
            _context = context;
            _response = new ResponseDto();
        }

        // GET: api/TipoAlimentacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoAlimentacion>>> GetTipoAlimentacion()
        {
            try
            {
                var tipoalimentos = await _tipoAlimentacionRepositorio.GetTipoAlimentacion();
                if(tipoalimentos == null || !tipoalimentos.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron los Tipos de Alimentos";
                    return NotFound(_response);
                }
                return Ok(tipoalimentos);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener los Tipos de Alimentos";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener todos los Tipos de Alimentos", Details = ex.Message });
            }
        }

        // GET: api/TipoAlimentacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoAlimentacion>> GetTipoAlimentacion(int id)
        {
            if (!await _tipoAlimentacionRepositorio.TipoAlimentacionExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Tipo de Alimento no encontrada. ";
                return Ok(_response);
            }
            try
            {
                var tipoalimentos = await _tipoAlimentacionRepositorio.GetTipoAlimentacionById(id);
                if(tipoalimentos != null)
                {
                    _response.Result = tipoalimentos;
                    _response.DisplayMessage = "Tipo de Alimento encontrada correctamente. ";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Tipo de Alimento no encontrada. ";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al obtener el Tipo de Alimento. ");
                return StatusCode(500, _response);
            }
        }

        // PUT: api/TipoAlimentacion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoAlimentacion(int id, DtoTipoAlimentacion tipoAlimentacionDto)
        {
            if(!await _tipoAlimentacionRepositorio.TipoAlimentacionExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Tipo de Alimento no encontrada. ";
                return NotFound(_response);
            }
            try
            {
                var tipoalimentos = await _tipoAlimentacionRepositorio.Update(tipoAlimentacionDto);
                _response.Result = tipoalimentos;
                _response.DisplayMessage = "Tipo de Alimento actualizada correctamente. ";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.DisplayMessage = "Error al actualizar el Tipo de Alimento. ";
                return BadRequest(_response);
            }
        }

        // POST: api/TipoAlimentacion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoAlimentacion>> PostTipoAlimentacion(DtoTipoAlimentacion tipoAlimentacionDto)
        {
            try
            {
                DtoTipoAlimentacion tipoalimento = await _tipoAlimentacionRepositorio.Create(tipoAlimentacionDto);
                return StatusCode(201, new { Message = "Tipo de Alimento creada correctamente", Result = tipoalimento });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el Tipo de Alimento. ");
                return BadRequest(new { Message = "Error al crear el Tipo de Alimento", Details = ex.Message });
            }
        }

        // DELETE: api/TipoAlimentacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoAlimentacion(int id)
        {
            try
            {
                bool deleted = await _tipoAlimentacionRepositorio.DeleteTipoAlimentacion(id);
                if (deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "Tipo de Alimento no encontrada para eliminar." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el Tipo de Alimento. ");
                return StatusCode(500, new { Message = "Error al eliminar el Tipo de Alimento", Details = ex.Message });
            }
        }

        private bool TipoAlimentacionExists(int id)
        {
            return _context.TipoAlimentacion.Any(e => e.Id == id);
        }
    }
}
