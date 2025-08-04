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
    public class CursoCapacitacionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICursoCapacitacionRepositorio _cursoCapacitacionRepositorio;
        private readonly ILogger<CursoCapacitacionController> _logger;
        protected ResponseDto _response;

        public CursoCapacitacionController(ApplicationDbContext context, ILogger<CursoCapacitacionController> logger, ICursoCapacitacionRepositorio cursoCapacitacionRepositorio)
        {
            _context = context;
            _logger = logger;
            _cursoCapacitacionRepositorio = cursoCapacitacionRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/CursoCapacitacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CursoCapacitacion>>> GetCursoCapacitacion()
        {
            try
            {
                var cursoCapacitaciones = await _cursoCapacitacionRepositorio.GetCursoCapacitacion();
                if (cursoCapacitaciones == null || !cursoCapacitaciones.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron cursos de capacitación.";
                    return NotFound(_response);
                }
                return Ok(cursoCapacitaciones);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener los cursos de capacitación.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener todos los Cursos de Capacitación", Details = ex.Message });
            }
        }

        // GET: api/CursoCapacitacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CursoCapacitacion>> GetCursoCapacitacion(int id)
        {
            if(!await _cursoCapacitacionRepositorio.CursoCapacitacionExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Curso de capacitación encontrado.";
                return Ok(_response);
            }
            try
            {
                var cursoCapacitacion = await _cursoCapacitacionRepositorio.GetCursoCapacitacionById(id);
                if (cursoCapacitacion != null)
                {
                    _response.Result = cursoCapacitacion;
                    _response.DisplayMessage = "Curso de capacitación encontrado.";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Curso de capacitación no encontrado.";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al obtener el curso de capacitación.");
                return StatusCode(500, _response);
            }
        }

        // PUT: api/CursoCapacitacion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCursoCapacitacion(int id, DtoCursoCapacitacion cursoCapacitacionDto)
        {
            if(!await _cursoCapacitacionRepositorio.CursoCapacitacionExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Curso de capacitación no encontrado.";
                return NotFound(_response);
            }
            try
            {
                var updatedCursoCapacitacion = await _cursoCapacitacionRepositorio.Update(cursoCapacitacionDto);
                _response.Result = updatedCursoCapacitacion;
                _response.DisplayMessage = "Curso de capacitación actualizado correctamente.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.DisplayMessage = "Error al actualizar el curso de capacitación.";
                return BadRequest(_response);
            }
        }
        // POST: api/CursoCapacitacion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CursoCapacitacion>> PostCursoCapacitacion(DtoCursoCapacitacion cursoCapacitacionDto)
        {
            try
            {
                DtoCursoCapacitacion createdCursoCapacitacion = await _cursoCapacitacionRepositorio.Create(cursoCapacitacionDto);
                return StatusCode(201, new { Message = "Curso de capacitación creado correctamente.", Result = createdCursoCapacitacion });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el curso de capacitación.");
                return BadRequest(new { Message = "Error al crear el curso de capacitación.", Details = ex.Message });
            }
        }

        // DELETE: api/CursoCapacitacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCursoCapacitacion(int id)
        {
            try
            {
                bool deleted = await _cursoCapacitacionRepositorio.DeleteCursoCapacitacion(id);
                if (!deleted)
                {
                    return NotFound();
                }
                else
                {
                    return NotFound(new { Message = "Curso de capacitación no encontrado." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el curso de capacitación.");
                return StatusCode(500, new { Message = "Error al eliminar el curso de capacitación.", Details = ex.Message });
            }
        }

        private bool CursoCapacitacionExists(int id)
        {
            return _context.CursoCapacitacion.Any(e => e.Id == id);
        }
    }
}
