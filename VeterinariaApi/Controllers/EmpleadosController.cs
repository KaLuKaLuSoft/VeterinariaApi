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
    public class EmpleadosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmpleadoRepositorio _empleadoRepositorio;
        private readonly ILogger<EmpleadosController> _logger;
        protected ResponseDto _response;

        public EmpleadosController(ApplicationDbContext context, ILogger<EmpleadosController> logger, IEmpleadoRepositorio empleadoRepositorio)
        {
            _context = context;
            _logger = logger;
            _empleadoRepositorio = empleadoRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/Empleados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleados>>> GetEmpleados()
        {
            try
            {
                var empleados = await _empleadoRepositorio.GetEmpleados();
                if(empleados == null || !empleados.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron empleados.";
                    return NotFound(_response);
                }
                return Ok(empleados);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener los empleados.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener todos los empleados", Details = ex.Message });
            }
        }

        // GET: api/Empleados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Empleados>> GetEmpleados(int id)
        {
            if(!await _empleadoRepositorio.EmpleadoExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Empleado no encontrado.";
                return NotFound(_response);
            }
            try
            {
                var empleado = await _empleadoRepositorio.GetEmpleadoById(id);
                if(empleado != null)
                {
                    _response.Result = empleado;
                    _response.DisplayMessage = "Empleado encontrado exitosamente.";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Empleado no encontrado.";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al obtener el empleado.");
                return StatusCode(500, _response);
            }
        }

        // PUT: api/Empleados/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpleados(int id, DtoEmpleado empleadosDto)
        {
            if(!await _empleadoRepositorio.EmpleadoExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Empleado no encontrado.";
                return NotFound(_response);
            }
            try
            {
                var empleado = await _empleadoRepositorio.Update(empleadosDto);
                _response.Result = empleado;
                _response.DisplayMessage = "Empleado actualizado exitosamente.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al actualizar el empleado.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
        }

        // POST: api/Empleados
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Empleados>> PostEmpleados(DtoEmpleado empleadosDto)
        {
            try
            {
                DtoEmpleado empleado = await _empleadoRepositorio.Create(empleadosDto);
                return StatusCode(201, new { Message = "Empleado creado exitosamente.", Data = empleado });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el empleado.");
                return BadRequest(new { Message = "Error al crear el empleado", Details = ex.Message });
            }
        }

        // DELETE: api/Empleados/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpleados(int id)
        {
            try
            {
                bool deleted = await _empleadoRepositorio.DeleteEmpleado(id);
                if(deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "Empleado no encontrado." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el empleado.");
                return StatusCode(600, new { Message = "Error al eliminar el empleado", Details = ex.Message });
            }
        }

        private bool EmpleadosExists(int id)
        {
            return _context.Empleados.Any(e => e.Id == id);
        }
    }
}
