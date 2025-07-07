using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;
using VeterinariaApi.Models;

namespace VeterinariaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoTurnoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITipoTurnoRepositorio _tipoTurnoRepositorio;
        private readonly ILogger<TipoTurnoController> _logger;
        protected ResponseDto _response;

        public TipoTurnoController(ApplicationDbContext context, ILogger<TipoTurnoController> logger, ITipoTurnoRepositorio tipoTurnoRepositorio)
        {
            _context = context;
            _logger = logger;
            _tipoTurnoRepositorio = tipoTurnoRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/TipoTurno
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoTurno>>> GetTipoTurno()
        {
            try
            {
                var tipoturno = await _tipoTurnoRepositorio.GetTipoTurno();
                if(tipoturno == null || !tipoturno.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron tipos de turno.";
                    return NotFound(_response);
                }
                return Ok(tipoturno);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener los tipos de turno.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener todos los Tipos de Turno", Details = ex.Message });
            }
        }

        // GET: api/TipoTurno/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoTurno>> GetTipoTurno(int id)
        {
            if(!await _tipoTurnoRepositorio.TipoTurnoExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Tipo de turno no encontrado.";
                return Ok(_response);
            }
            try
            {
                var tipoturno = await _tipoTurnoRepositorio.GetTipoTurnoById(id);
                if(tipoturno != null)
                {
                    _response.Result = tipoturno;
                    _response.DisplayMessage = "Tipo de turno encontrado correctamente.";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Tipo de turno no encontrado.";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError("Error al obtener el tipo de turno por ID", ex);
                return StatusCode(500, _response);
            }
        }

        // PUT: api/TipoTurno/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoTurno(int id, DtoTipoTurno tipoTurnoDto)
        {
            if(!await _tipoTurnoRepositorio.TipoTurnoExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Tipo de turno no encontrado.";
                return NotFound(_response);
            }
            try
            {
                var tipoturno = await _tipoTurnoRepositorio.Update(tipoTurnoDto);
                _response.Result = tipoturno;
                _response.DisplayMessage = "Tipo de turno actualizado correctamente.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al actualizar el tipo de turno";
                return BadRequest(_response);
            }
        }

        // POST: api/TipoTurno
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoTurno>> PostTipoTurno(DtoTipoTurno tipoTurnoDto)
        {
            try
            {
                DtoTipoTurno tipoturno = await _tipoTurnoRepositorio.Create(tipoTurnoDto);
                return StatusCode(201, new { Message = "Tipo de turno creado correctamente", Data = tipoturno });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el tipo de turno");
                return BadRequest(new { Message = "Error al crear el tipo de turno", Details = ex.Message });
            }
        }

        // DELETE: api/TipoTurno/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoTurno(int id)
        {
            try
            {
                bool deleted = await _tipoTurnoRepositorio.DeleteTipoTurno(id);
                if(deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "Tipo de turno no encontrado." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar tipo de turno");
                return StatusCode(500, new { Message = "Error al eliminar el tipo de turno", Details = ex.Message });
            }
        }

        private bool TipoTurnoExists(int id)
        {
            return _context.TipoTurno.Any(e => e.Id == id);
        }
    }
}
