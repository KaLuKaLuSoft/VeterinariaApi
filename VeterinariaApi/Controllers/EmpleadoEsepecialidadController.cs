using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;
using VeterinariaApi.Migrations;
using VeterinariaApi.Models;

namespace VeterinariaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoEsepecialidadController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EmpleadoEsepecialidadController> _logger;
        protected ResponseDto _response;
        private readonly IEmpleadoEspecialidadRepositorio _empleadoespecialidadRepositorio;
        public EmpleadoEsepecialidadController(ApplicationDbContext context, ILogger<EmpleadoEsepecialidadController> logger, IEmpleadoEspecialidadRepositorio empleadoEspecialidadRepositorio)
        {
            _context = context;
            _logger = logger;
            _empleadoespecialidadRepositorio = empleadoEspecialidadRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/EmpleadoEsepecialidad
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpleadoEsepecialidad>>> GetEmpleadoEsepecialidad()
        {
            try
            {
                var empleadoEspecialidades = await _empleadoespecialidadRepositorio.GetEmpleadoEspecialidad();
                if (empleadoEspecialidades == null || !empleadoEspecialidades.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron especialidades para los empleados.";
                    return NotFound(_response);
                }
                return Ok(empleadoEspecialidades);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener las especialidades de los empleados.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener las especialidades de los empleados", Details = ex.Message });
            }
        }
        // GET: api/EmpleadoEsepecialidad/5
        [HttpGet("{empleadoId}/{especialidadId}")]
        public async Task<ActionResult<EmpleadoEsepecialidad>> GetEmpleadoEsepecialidad(int empleadoId, int especialidadId)
        {
            if(!await _empleadoespecialidadRepositorio.EmpleadoEspecialidadExists(empleadoId, especialidadId))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "No se encontró la especialidad para el empleado especificado.";
                return NotFound(_response);
            }
            try
            {
                var empleadoEspecialidad = await _empleadoespecialidadRepositorio.GetEmpleadoEspecialidadById(empleadoId,especialidadId);
                if (empleadoEspecialidad != null)
                {
                    _response.Result = empleadoEspecialidad;
                    _response.DisplayMessage = "Especialidad del empleado encontrada correctamente.";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontró la especialidad para el empleado especificado.";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al obtener la especialidad del empleado.");
                return StatusCode(500, _response);
            }
        }

        // PUT: api/EmpleadoEsepecialidad/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{empleadoId}/{especialidadId}")]
        public async Task<IActionResult> PutEmpleadoEsepecialidad(int empleadoId, int especialidadId, [FromBody] DtoEmpleadoEspecialidad empleadoEspecialidadDto)
        {
            if(!await _empleadoespecialidadRepositorio.EmpleadoEspecialidadExists(empleadoId, especialidadId))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "No se encontró la especialidad para el empleado especificado.";
                return NotFound(_response);
            }
            try
            {
                var empleadoEspecialidad = await _empleadoespecialidadRepositorio.Update(empleadoEspecialidadDto);
                _response.Result = empleadoEspecialidad;
                _response.DisplayMessage = "Especialidad del empleado actualizada correctamente.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al actualizar la especialidad del empleado.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, _response);
            }
        }

        // POST: api/EmpleadoEsepecialidad
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmpleadoEsepecialidad>> PostEmpleadoEsepecialidad(DtoEmpleadoEspecialidad empleadoEsepecialidadDto)
        {
            try
            {
                DtoEmpleadoEspecialidad empleadoEspecialidad = await _empleadoespecialidadRepositorio.Create(empleadoEsepecialidadDto);
                return StatusCode(201, new { Message = "Especialidad del empleado creada correctamente.", Data = empleadoEspecialidad });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la especialidad del empleado.");
                return StatusCode(500, new { Message = "Error al crear la especialidad del empleado", Details = ex.Message });
            }
        }

        // DELETE: api/EmpleadoEsepecialidad/5
        [HttpDelete("{empleadoId}/{especialidadId}")]
        public async Task<IActionResult> DeleteEmpleadoEsepecialidad(int empleadoId, int especialidadId)
        {
            try
            {
                bool deleted = await _empleadoespecialidadRepositorio.DeleteEmpleadoEspecialidad(empleadoId, especialidadId);
                if (deleted)
                {
                    return  NoContent();
                }
                else
                {
                    return NotFound(new { Message = "No se encontró la especialidad para el empleado especificado." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la especialidad del empleado.");
                return StatusCode(500, new { Message = "Error al eliminar la especialidad del empleado", Details = ex.Message });
            }
        }

        private bool EmpleadoEsepecialidadExists(int id)
        {
            return _context.EmpleadoEsepecialidad.Any(e => e.EmpleadoId == id);
        }
    }
}
