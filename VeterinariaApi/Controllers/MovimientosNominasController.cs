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
    public class MovimientosNominasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MovimientosNominasController> _logger;
        protected ResponseDto _response;
        private readonly IMovimientoNominaRepositorio _movimientonominaRepositorio;

        public MovimientosNominasController(ApplicationDbContext context, ILogger<MovimientosNominasController> logger, IMovimientoNominaRepositorio movimientonominaRepositorio)
        {
            _context = context;
            _logger = logger;
            _movimientonominaRepositorio = movimientonominaRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/MovimientosNominas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimientosNomina>>> GetMovimientosNomina()
        {
            try
            {
                var movimientosNomina = await _movimientonominaRepositorio.GetMovimientoNomina();
                if (movimientosNomina == null || !movimientosNomina.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron movimientos de nómina.";
                    return NotFound(_response);
                }
                return Ok(movimientosNomina);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener los movimientos de nómina.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener todos los Movimientos de Nómina", Details = ex.Message });
            }
        }

        // GET: api/MovimientosNominas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovimientosNomina>> GetMovimientosNomina(int id)
        {
            if(!await _movimientonominaRepositorio.MovimientoNominaExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "El movimiento de nómina no existe.";
                return Ok(_response);
            }
            try
            {
                var movimientoNomina = await _movimientonominaRepositorio.GetMovimientoNominaById(id);
                if (movimientoNomina != null)
                {
                    _response.Result = movimientoNomina;
                    _response.DisplayMessage = "Se encontró el movimiento de nómina.";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontró el movimiento de nómina.";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al obtener el movimiento de nómina.");
                return StatusCode(500, _response);
            }
        }
        // PUT: api/MovimientosNominas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovimientosNomina(int id, DtoMovimientosNomina movimientosNominaDto)
        {
            if(!await _movimientonominaRepositorio.MovimientoNominaExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "El movimiento de nómina no existe.";
                return NotFound(_response);
            }
            try
            {
                var movimientoNomina = await _movimientonominaRepositorio.Update(movimientosNominaDto);
                _response.Result = movimientoNomina;
                _response.DisplayMessage = "Movimiento de nómina actualizado correctamente.";
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.DisplayMessage = "Error al actualizar el movimiento de nómina.";
                return BadRequest(_response);
            }
        }

        // POST: api/MovimientosNominas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MovimientosNomina>> PostMovimientosNomina(DtoMovimientosNomina movimientosNominaDto)
        {
            try
            {
                DtoMovimientosNomina nuevoMovimientoNomina = await _movimientonominaRepositorio.Create(movimientosNominaDto);
                return StatusCode(201, new {Message = "Movimiento de nómina creado correctamente.", Data = nuevoMovimientoNomina });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el movimiento de nómina.");
                return BadRequest(new { Message = "Error al crear el movimiento de nómina.", Details = ex.Message });
            }
        }

        // DELETE: api/MovimientosNominas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimientosNomina(int id)
        {
            try
            {
                bool deleted = await _movimientonominaRepositorio.DeleteMovimientoNomina(id);
                if(deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "Movimiento de nómina no encontrado." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el movimiento de nómina.");
                return StatusCode(500, new { Message = "Error al eliminar el movimiento de nómina.", Details = ex.Message });
            }
        }

        private bool MovimientosNominaExists(int id)
        {
            return _context.MovimientosNomina.Any(e => e.Id == id);
        }
    }
}
