using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;
using VeterinariaApi.Models;

namespace VeterinariaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluacionEmpleadosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EvaluacionEmpleadosController> _logger;
        protected ResponseDto _response;
        private readonly IEvaluacionEmpleadoRepositorio _evaluacionEmpleadoRepositorio;

        public EvaluacionEmpleadosController(ApplicationDbContext context, ILogger<EvaluacionEmpleadosController> logger, IEvaluacionEmpleadoRepositorio evaluacionEmpleadoRepositorio)
        {
            _context = context;
            _logger = logger;
            _evaluacionEmpleadoRepositorio = evaluacionEmpleadoRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/EvaluacionEmpleados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EvaluacionEmpleado>>> GetEvaluacionEmpleado()
        {
            try
            {
                var evaluaciones = await _evaluacionEmpleadoRepositorio.GetEvaluacionEmpleado();
                if (evaluaciones == null || !evaluaciones.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron evaluaciones de empleados.";
                    return NotFound(_response);
                }
                return Ok(evaluaciones);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener las evaluaciones de empleados.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener todas las Evaluaciones de Empleados", Details = ex.Message });
            }
        }

        // GET: api/EvaluacionEmpleados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EvaluacionEmpleado>> GetEvaluacionEmpleado(int id)
        {
            if(!await _evaluacionEmpleadoRepositorio.EvaluacionEmpleadoExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Evaluación de empleado no encontrada.";
                return NotFound(_response);
            }
            try
            {
                var evaluacionEmpleado = await _evaluacionEmpleadoRepositorio.GetEvaluacionEmpleadoById(id);
                if (evaluacionEmpleado != null)
                {
                    _response.Result = evaluacionEmpleado;
                    _response.DisplayMessage = "Evaluación de empleado encontrada.";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Evaluación de empleado no encontrada.";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al obtener la evaluación de empleado. ");
                return StatusCode(500, _response);
            }
        }

        // PUT: api/EvaluacionEmpleados/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvaluacionEmpleado(int id, DtoEvaluacionEmpleado evaluacionEmpleadoDto)
        {
            if(!await _evaluacionEmpleadoRepositorio.EvaluacionEmpleadoExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Evaluación de empleado no encontrada.";
                return NotFound(_response);
            }
            try
            {
                var evaluacionempleados = await _evaluacionEmpleadoRepositorio.Update(evaluacionEmpleadoDto);
                _response.Result = evaluacionempleados;
                _response.DisplayMessage = "Evaluacion Empleado actualizado correctamente. ";
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.DisplayMessage = "Error al actualizar Evaluacion Empleado. ";
                return BadRequest(_response);
            }
        }

        // POST: api/EvaluacionEmpleados
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EvaluacionEmpleado>> PostEvaluacionEmpleado(DtoEvaluacionEmpleado evaluacionEmpleadoDto)
        {
            try
            {
                DtoEvaluacionEmpleado evaluacionEmpleados = await _evaluacionEmpleadoRepositorio.Create(evaluacionEmpleadoDto);
                return StatusCode(201, new { Message = "Evaluación Empleado creada correctamente. ", Data = evaluacionEmpleados });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear evaluación empleado. ");
                return BadRequest(new { Message = "Error al crear evaluación empleado. ", Details = ex.Message });
            }
        }

        // DELETE: api/EvaluacionEmpleados/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvaluacionEmpleado(int id)
        {
            try
            {
                bool deleted = await _evaluacionEmpleadoRepositorio.DeleteEvaluacionEmpleado(id);
                if (deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "Evaluación Empleado no encontrada. " });
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar evaluación empleado. ");
                return StatusCode(500, new { Message = "Error al eliminar evaluación empleado. ", Details = ex.Message });
            }
        }

        private bool EvaluacionEmpleadoExists(int id)
        {
            return _context.EvaluacionEmpleado.Any(e => e.Id == id);
        }
    }
}
