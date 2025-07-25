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
    public class CriteriosEvaluacionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICriterioEvaluacionRepositorio _criterioEvaluacionRepositorio;
        private readonly ILogger<CriteriosEvaluacionController> _logger;
        protected ResponseDto _response;
        public CriteriosEvaluacionController(ApplicationDbContext context, ILogger<CriteriosEvaluacionController> logger, ICriterioEvaluacionRepositorio criterioEvaluacionRepositorio)
        {
            _context = context;
            _logger = logger;
            _criterioEvaluacionRepositorio = criterioEvaluacionRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/CriteriosEvaluacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CriteriosEvaluacion>>> GetCriterioEvaluacion()
        {
            try
            {
                var criterioevaluaciones = await _criterioEvaluacionRepositorio.GetCriterioEvaluacion();
                if(criterioevaluaciones == null || !criterioevaluaciones.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron CriteriosEvaluaciones. ";
                    return NotFound(_response);
                }
                return Ok(criterioevaluaciones);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener CriterioEvaluaciones. ";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener todos los CriteriosEvaluaciones. ", Details = ex.Message });
            }
        }

        // GET: api/CriteriosEvaluacion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CriteriosEvaluacion>> GetCriteriosEvaluacion(int id)
        {
            if(!await _criterioEvaluacionRepositorio.CriterioEvaluacionExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "CriterioEvalacion no encontrada. ";
                return Ok(_response);
            }
            try
            {
                var criterioevaluaciones = await _criterioEvaluacionRepositorio.GetCriterioEvaluacionById(id);
                if(criterioevaluaciones != null)
                {
                    _response.Result = criterioevaluaciones;
                    _response.DisplayMessage = "CriterioEvaluacion encontrada correctamente. ";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "CriterioEvaluacion no encontrada. ";
                    return NotFound(_response);
                }
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al obtener CriterioEvaluacion. ");
                return StatusCode(500, _response);
            }
        }

        // PUT: api/CriteriosEvaluacion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCriteriosEvaluacion(int id, DtoCriterioEvaluacion criteriosEvaluacionDto)
        {
            if(!await _criterioEvaluacionRepositorio.CriterioEvaluacionExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "CriterioEvaluacion no encontrada. ";
                return NotFound(_response);
            }
            try
            {
                var criterioevaluaciones = await _criterioEvaluacionRepositorio.Update(criteriosEvaluacionDto);
                _response.Result = criterioevaluaciones;
                _response.DisplayMessage = "CriterioEvaluacion actualizada correctamente. ";
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.DisplayMessage = "Error al actualizar CriterioEvaluacion. ";
                return BadRequest(_response);
            }
        }

        // POST: api/CriteriosEvaluacion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CriteriosEvaluacion>> PostCriteriosEvaluacion(DtoCriterioEvaluacion criteriosEvaluacionDto)
        {
            try
            {
                DtoCriterioEvaluacion criterioEvaluacion = await _criterioEvaluacionRepositorio.Create(criteriosEvaluacionDto);
                return StatusCode(201, new { Message = "CriterioEvaluacion creada correctamente. ", Data = criterioEvaluacion });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error al crear CriterioEvaluacion. ");
                return BadRequest(new { Message = "Error al crear CriteriorEvaluacion. ", Details = ex.Message });
            }
        }

        // DELETE: api/CriteriosEvaluacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCriteriosEvaluacion(int id)
        {
            try
            {
                bool deleted = await _criterioEvaluacionRepositorio.DeleteCriterioEvaluacion(id);
                if (deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "CriterioEvaluacion no encontrada. " });
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar CriterioEvaluacion. ");
                return StatusCode(500, new { Message = "Error al eliminar la CriterioEvaluacion", Details = ex.Message });
            }
        }

        private bool CriteriosEvaluacionExists(int id)
        {
            return _context.CriterioEvaluacion.Any(e => e.Id == id);
        }
    }
}
