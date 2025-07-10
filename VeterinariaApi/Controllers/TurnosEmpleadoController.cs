using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
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
    public class TurnosEmpleadoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TurnosEmpleadoController> _logger;
        private readonly ResponseDto _response;
        private readonly ITurnosEmpleadoRepositorio _turnosEmpleadoRepositorio;
        public TurnosEmpleadoController(ApplicationDbContext context, ILogger<TurnosEmpleadoController> logger, ITurnosEmpleadoRepositorio turnosEmpleadoRepositorio)
        {
            _context = context;
            _logger = logger;
            _response = new ResponseDto();
            _turnosEmpleadoRepositorio = turnosEmpleadoRepositorio;
        }

        // GET: api/TurnosEmpleado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TurnosEmpleado>>> GetTurnosEmpleado()
        {
            try
            {
                var turnosEmpleados = await _turnosEmpleadoRepositorio.GetTurnosEmpleado();
                if (turnosEmpleados == null || !turnosEmpleados.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron turnos de empleados.";
                    return NotFound(_response);
                }
                return  Ok(turnosEmpleados);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener los turnos de empleados.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener todos los Turnos de Empleados", Details = ex.Message });
            }
        }

        // GET: api/TurnosEmpleado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TurnosEmpleado>> GetTurnosEmpleado(int id)
        {
            if(!await _turnosEmpleadoRepositorio.TurnosEmpleadoExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Turno de empleado no encontrado.";
                return Ok(_response);
            }
            try
            {
                var turnosEmpleado = await _turnosEmpleadoRepositorio.GetTurnosEmpleadoById(id);
                if (turnosEmpleado != null)
                {
                    _response.Result = turnosEmpleado;
                    _response.DisplayMessage = "Turno de empleado encontrado correctamente.";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Turno de empleado no encontrado.";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al obtener el turno de empleado.");
                return StatusCode(500, _response);
            }
        }

        // PUT: api/TurnosEmpleado/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTurnosEmpleado(int id, DtoTurnosEmpleado turnosEmpleadoDto)
        {
            if(!await _turnosEmpleadoRepositorio.TurnosEmpleadoExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Turno de empleado no encontrado.";
                return NotFound(_response);
            }
            try
            {
                var turnoempleado = await _turnosEmpleadoRepositorio.Update(turnosEmpleadoDto);
                _response.Result = turnoempleado;
                _response.DisplayMessage = "TurnosEmpleado actualizado correctamente.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.DisplayMessage = "Error al actualizar el turno de empleado.";
                return BadRequest(_response);
            }
        }

        // POST: api/TurnosEmpleado
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TurnosEmpleado>> PostTurnosEmpleado(DtoTurnosEmpleado turnosEmpleadoDto)
        {
            try
            {
                DtoTurnosEmpleado dtoTurnosEmpleado = await _turnosEmpleadoRepositorio.Create(turnosEmpleadoDto);

                return StatusCode(201, new { Message = "Turno de empleado creado correctamente.", Data = dtoTurnosEmpleado });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error al crear el turno de empleado");
                return BadRequest(new { Message = "Error al crear el turno de empleado: ", Details = ex.Message });
            }
        }

        // DELETE: api/TurnosEmpleado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTurnosEmpleado(int id)
        {
            try
            {
                bool deleted = await _turnosEmpleadoRepositorio.DeleteTurnosEmpleado(id);
                if(deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "Turno de empleado no encontrado." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el turno de empleado");
                return StatusCode(500, new { Message = "Error al eliminar el turno de empleado", Details = ex.Message });
            }
        }

        private bool TurnosEmpleadoExists(int id)
        {
            return _context.TurnosEmpleado.Any(e => e.Id == id);
        }
    }
}
