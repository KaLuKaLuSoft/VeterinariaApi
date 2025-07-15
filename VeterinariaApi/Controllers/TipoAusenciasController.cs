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
    public class TipoAusenciasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITipoAusenciaRepositorio _tipoAusenciaRepositorio;
        private readonly ILogger<TipoAusenciasController> _logger;
        protected ResponseDto _response;

        public TipoAusenciasController(ApplicationDbContext context, ILogger<TipoAusenciasController> logger, ITipoAusenciaRepositorio tipoAusenciaRepositorio)
        {
            _context = context;
            _tipoAusenciaRepositorio = tipoAusenciaRepositorio;
            _logger = logger;
            _response = new ResponseDto();
        }

        // GET: api/TipoAusencias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoAusencia>>> GetTipoAusencia()
        {
            try
            {
                var tipoAusencias = await _tipoAusenciaRepositorio.GetTipoAusencia();
                if (tipoAusencias == null || !tipoAusencias.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron tipos de ausencia.";
                    return NotFound(_response);
                }
                return Ok(tipoAusencias);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener los tipos de ausencia.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener todos los tipos de ausencia", Details = ex.Message });
            }
        }

        // GET: api/TipoAusencias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoAusencia>> GetTipoAusencia(int id)
        {
            if(!await _tipoAusenciaRepositorio.TipoAusenciaExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Tipo de ausencia no encontrado.";
                return NotFound(_response);
            }
            try
            {
                var tipoAusencia = await _tipoAusenciaRepositorio.GetTipoAusenciaById(id);
                if(tipoAusencia != null)
                {
                    _response.Result = tipoAusencia;
                    _response.DisplayMessage = "Tipo de ausencia no encontrado.";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Tipo de ausencia no encontrado.";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al obtener el tipo de ausencia.");
                return StatusCode(500, _response);
            }
        }

        // PUT: api/TipoAusencias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoAusencia(int id, DtoTipoAusencia tipoAusenciaDto)
        {
            if(!await _tipoAusenciaRepositorio.TipoAusenciaExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Tipo de ausencia no encontrado.";
                return NotFound(_response);
            }
            try
            {
                var tipoAusencias = await _tipoAusenciaRepositorio.Update(tipoAusenciaDto);
                _response.Result = tipoAusencias;
                _response.DisplayMessage = "Tipo de ausencia actualizado correctamente.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.DisplayMessage = "Error al actualizar el tipo de ausencia.";
                return BadRequest(_response);
            }
        }

        // POST: api/TipoAusencias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoAusencia>> PostTipoAusencia(DtoTipoAusencia tipoAusenciaDto)
        {
            try
            {
                DtoTipoAusencia tipoAusencia = await _tipoAusenciaRepositorio.Create(tipoAusenciaDto);
                return StatusCode(201, new { Message = "Tipo de ausencia creado correctamente", Data = tipoAusencia });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el tipo de ausencia.");
                return BadRequest(new { Message = "Error al crear el tipo de ausencia", Details = ex.Message });
            }
        }

        // DELETE: api/TipoAusencias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoAusencia(int id)
        {
            try
            {
                bool deleted = await _tipoAusenciaRepositorio.DeleteTipoAusencia(id);
                if(deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "Tipo de ausencia no encontrado." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el tipo de ausencia.");
                return StatusCode(500, new { Message = "Error al eliminar el tipo de ausencia", Details = ex.Message });
            }
        }

        private bool TipoAusenciaExists(int id)
        {
            return _context.TipoAusencia.Any(e => e.Id == id);
        }
    }
}
