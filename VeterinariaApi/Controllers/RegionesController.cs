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
    public class RegionesController : ControllerBase
    {
        private readonly IRegionesRepositorio _regionesRepositorio;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RegionesController> _logger;
        protected ResponseDto _response;

        public RegionesController(ApplicationDbContext context, ILogger<RegionesController> logger, IRegionesRepositorio regionesRepositorio)
        {
            _context = context;
            _logger = logger;
            _regionesRepositorio = regionesRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/Regiones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Regiones>>> GetRegiones()
        {
            try
            {
                var regiones = await _regionesRepositorio.GetRegiones();
                
                if(regiones == null || !regiones.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron regiones.";
                    return NotFound(_response);
                }
                return Ok(regiones);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener las regiones.";
                _response.ErrorMessages = new List<string> { ex.Message};
                return StatusCode(500, new { Message = "Error al obtener todos las regiones", Details = ex.Message });
            }
        }

        // GET: api/Regiones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Regiones>> GetRegiones(int id)
        {
            if(!await _regionesRepositorio.RegionesExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Región no encontrada.";
                return NotFound(_response);
            }
            try
            {
                var region = await _regionesRepositorio.GetRegionesById(id);
                if(region != null)
                {
                    _response.Result = region;
                    _response.DisplayMessage = "Región encontrada.";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Región no encontrada.";
                    return NotFound(_response);
                }
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al obtener la región con ID");
                return StatusCode(500,_response);
            }
        }

        // PUT: api/Regiones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegiones(int id, DtoRegiones regionesDto)
        {
            if(!await _regionesRepositorio.RegionesExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Región no encontrada.";
                return NotFound(_response);
            }
            try
            {
                var region = await _regionesRepositorio.Update(regionesDto);
                _response.Result = region;
                _response.DisplayMessage = "Región actualizada correctamente.";
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al actualizar la región.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
        }

        // POST: api/Regiones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Regiones>> PostRegiones(DtoRegiones regionesDto)
        {
            try
            {
                DtoRegiones regiones = await _regionesRepositorio.Create(regionesDto);
                return StatusCode(201, new {Message = "Región creada correctamente.", Data = regiones });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la región");
                return BadRequest(new {Message = "Error al crear la región.", Details = ex.Message});
            }
        }

        // DELETE: api/Regiones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegiones(int id)
        {
            try
            {
                bool deleted = await _regionesRepositorio.DeleteRegiones(id);
                if(deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "Región no encontrada." }); 
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la región");
                return StatusCode(600, new { Message = "Error al eliminar la región.", Details = ex.Message });
            }
        }

        private bool RegionesExists(int id)
        {
            return _context.Regiones.Any(e => e.Id == id);
        }
    }
}
