using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;
using VeterinariaApi.Models;

namespace VeterinariaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadesMedicasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IEspecialidadMedicaRepositorio _especialidadMedicaRepositorio;
        private readonly ILogger<EspecialidadesMedicasController> _logger;
        protected ResponseDto _response;
        public EspecialidadesMedicasController(ApplicationDbContext context,ILogger<EspecialidadesMedicasController> logger, IEspecialidadMedicaRepositorio especialidadMedicaRepositorio)
        {
            _context = context;
            _logger = logger;
            _especialidadMedicaRepositorio = especialidadMedicaRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/EspecialidadesMedicas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EspecialidadesMedicas>>> GetEspecialidadesMedicas()
        {
            try
            {
                var especialidades = await _especialidadMedicaRepositorio.GetEspecialidades();
                if(especialidades == null || !especialidades.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron especialidades.";
                    return NotFound(_response);
                }
                return Ok(especialidades);
            }
            catch (Exception ex) 
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener las especialidades.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, _response);
            }
        }

        // GET: api/EspecialidadesMedicas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EspecialidadesMedicas>> GetEspecialidadesMedicas(int id)
        {
            if(!await _especialidadMedicaRepositorio.EspecialidadesExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Especialidad no encontrada";
                return NotFound(_response);
            }
            try
            {
                var especialidades = await _especialidadMedicaRepositorio.GetEspecialidadesById(id);
                if(especialidades != null)
                {
                    _response.Result = especialidades;
                    _response.DisplayMessage = "Especialidad encontrada";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Especialidad encontrada.";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al obtener la especialidad con ID");
                return StatusCode(500, _response);
            }  
        }

        // PUT: api/EspecialidadesMedicas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEspecialidadesMedicas(int id, DtoEpecialidadesMedicas especialidadesMedicasDto)
        {
            if(!await _especialidadMedicaRepositorio.EspecialidadesExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Especialidad No encontrada";
                return NotFound(_response);
            }
            try
            {
                var especialidades = await _especialidadMedicaRepositorio.Update(especialidadesMedicasDto);
                _response.Result = especialidades;
                _response.DisplayMessage = "Especialidad actualizado correctamente.";
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al actualizar la especialidad.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, _response);
            }
        }

        // POST: api/EspecialidadesMedicas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EspecialidadesMedicas>> PostEspecialidadesMedicas(DtoEpecialidadesMedicas especialidadesMedicasDto)
        {
            try
            {
                DtoEpecialidadesMedicas especialidades = await _especialidadMedicaRepositorio.Create(especialidadesMedicasDto);
                return StatusCode(201, new { Message = "Especialidad creada correctamente.", Data = especialidades});
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error al crear la especialidad.");
                return BadRequest(new { Message = "Error al crear la especialidad.", Details = ex.Message });
            }
        }

        // DELETE: api/EspecialidadesMedicas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspecialidadesMedicas(int id)
        {
            try
            {
                bool deleted = await _especialidadMedicaRepositorio.DeleteEspecialidades(id);
                if(deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "Especialidad no encontrada." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la especialidad.");
                return StatusCode(500, new { Message = "Error al eliminar la especialidad." });
            }
        }

        private bool EspecialidadesMedicasExists(int id)
        {
            return _context.EspecialidadesMedicas.Any(e => e.Id == id);
        }
    }
}
