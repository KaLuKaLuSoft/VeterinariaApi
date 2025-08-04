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
    public class EmpleadoCapacitacionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmpleadoCapacitacionRepositorio _empleadoCapacitacionRepositorio;
        private readonly ILogger<EmpleadoCapacitacionController> _logger;
        protected ResponseDto _response;
        public EmpleadoCapacitacionController(ApplicationDbContext context, ILogger<EmpleadoCapacitacionController> logger, IEmpleadoCapacitacionRepositorio empleadoCapacitacionRepositorio)
        {
            _context = context;
            _logger = logger;
            _empleadoCapacitacionRepositorio = empleadoCapacitacionRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/EmpleadoCapacitacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpleadoCapacitacion>>> GetEmpleadoCapacitacion()
        {
            try
            {
                var empleadoCapacitaciones = await _empleadoCapacitacionRepositorio.GetEmpleadoCapacitacion();
                if (empleadoCapacitaciones == null || !empleadoCapacitaciones.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron capacitaciones de empleados.";
                    return NotFound(_response);
                }
                return Ok(empleadoCapacitaciones);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener las capacitaciones de empleados.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener todas las capacitaciones de empleados", Details = ex.Message });
            }
        }

        // GET: api/EmpleadoCapacitacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmpleadoCapacitacion>> GetEmpleadoCapacitacion(int id)
        {
            if(!await _empleadoCapacitacionRepositorio.EmpleadoCapacitacionExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Capacitación de empleado no encontrada.";
                return Ok(_response);
            }
            try
            {
                var empleadoCapacitacion = await _empleadoCapacitacionRepositorio.GetEmpleadoCapacitacionById(id);
                if (empleadoCapacitacion != null)
                {
                    _response.Result = empleadoCapacitacion;
                    _response.DisplayMessage = "Capacitación de empleado encontrada.";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Capacitación de empleado no encontrada.";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex,"Error al obtener la capacitación de empleado.");
                return StatusCode(500, _response); 
            }
        }
        // PUT: api/EmpleadoCapacitacion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpleadoCapacitacion(int id, DtoEmpleadoCapacitacion empleadoCapacitacionDto)
        {
            if(!await _empleadoCapacitacionRepositorio.EmpleadoCapacitacionExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Capacitación de empleado no encontrada.";
                return Ok(_response);
            }
            try
            {
                var empleadoCapacitacion = await _empleadoCapacitacionRepositorio.Update(empleadoCapacitacionDto);
                _response.Result = empleadoCapacitacion;
                _response.DisplayMessage = "Capacitación de empleado actualizada correctamente.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al actualizar la capacitación de empleado.");
                return BadRequest(_response);
            }
        }

        // POST: api/EmpleadoCapacitacion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmpleadoCapacitacion>> PostEmpleadoCapacitacion(DtoEmpleadoCapacitacion empleadoCapacitacionDto)
        {
            try
            {
                DtoEmpleadoCapacitacion empleadoCapacitacion = await _empleadoCapacitacionRepositorio.Create(empleadoCapacitacionDto);
                return StatusCode(200, new { Message = "Capacitación de empleado creada correctamente.", Data = empleadoCapacitacion });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error al crear la capacitación de empleado.");
                return BadRequest(new { Message = "Error al crear la capacitación de empleado", Details = ex.Message });
            }
        }

        // DELETE: api/EmpleadoCapacitacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpleadoCapacitacion(int id)
        {
            try
            {
                bool deleted = await _empleadoCapacitacionRepositorio.DeleteEmpleadoCapacitacion(id);
                if(deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "Capacitación de empleado no encontrada." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la capacitación de empleado.");
                return StatusCode(500, new { Message = "Error al eliminar la capacitación de empleado", Details = ex.Message });
            }
        }

        private bool EmpleadoCapacitacionExists(int id)
        {
            return _context.EmpleadoCapacitacion.Any(e => e.Id == id);
        }
    }
}
