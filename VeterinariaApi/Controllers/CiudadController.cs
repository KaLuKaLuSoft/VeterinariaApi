using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class CiudadController : ControllerBase
    {
        private readonly ICiudadRepositorio _ciudadRepositorio;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CiudadController> _logger;
        protected ResponseDto _response;

        public CiudadController(ApplicationDbContext context, ILogger<CiudadController> logger, ICiudadRepositorio ciudadRepositorio)
        {
            _context = context;
            _logger = logger;
            _ciudadRepositorio = ciudadRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/Ciudad
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ciudad>>> GetCiudades()
        {
            try
            {
                var ciudades = await _ciudadRepositorio.GetCiudad();
                if(ciudades == null || !ciudades.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron ciudades.";
                    return NotFound(_response);
                }
                return Ok(ciudades);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener las ciudades.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener todos los Proveedores", Details = ex.Message });
            }
        }

        // GET: api/Ciudad/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ciudad>> GetCiudad(int id)
        {
            if(!await _ciudadRepositorio.CiudadExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Ciudad no encontrada.";
                return NotFound(_response);
            }
            try
            {
                var ciudad = await _ciudadRepositorio.GetCiudadById(id);
                if (ciudad != null)
                {
                    _response.Result = ciudad;
                    _response.DisplayMessage = "Ciudad encontrada correctamente.";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Ciudad no encontrada.";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al obtener la ciudad.");
                return StatusCode(500, _response);
            }
        }

        // PUT: api/Ciudad/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCiudad(int id, DtoCiudad ciudadDto)
        {
            if(!await _ciudadRepositorio.CiudadExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Ciudad no encontrada.";
                return NotFound(_response);
            }
            try
            {
                var region = await _ciudadRepositorio.Update(ciudadDto);
                _response.Result = region;
                _response.DisplayMessage = "Ciudad actualizada correctamente.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al actualizar la ciudad.";
                return BadRequest(_response);
            }
        }

        // POST: api/Ciudad
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ciudad>> PostCiudad(DtoCiudad ciudadDto)
        {
            try
            {
                DtoCiudad ciudad = await _ciudadRepositorio.Create(ciudadDto);
                return StatusCode(201, new { message = "Ciudad creada correctamente", Data = ciudad });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la ciudad");
                return BadRequest(new { message = "Error al crear la ciudad: ", Details = ex.Message });
            }
        }

        // DELETE: api/Ciudad/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCiudad(int id)
        {
            try
            {
                bool deleted = await _ciudadRepositorio.DeleteCiudad(id);
                if (deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { message = "Ciudad no encontrada." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la ciudad");
                return StatusCode(500, new { message = "Error al eliminar la ciudad", Details = ex.Message });
            }
        }

        private bool CiudadExists(int id)
        {
            return _context.Ciudades.Any(e => e.Id == id);
        }
    }
}
