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
    public class ActivosFijosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IActivosFijosRepositorio _activosFijosRepositorio;
        private readonly ILogger<ActivosFijosController> _logger;
        protected ResponseDto _response;
        public ActivosFijosController(ApplicationDbContext context, ILogger<ActivosFijosController> logger, IActivosFijosRepositorio activosFijosRepositorio)
        {
            _context = context;
            _logger = logger;
            _activosFijosRepositorio = activosFijosRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/ActivosFijos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivosFijos>>> GetActivosFijos()
        {
            try
            {
                var activofijos = await _activosFijosRepositorio.GetActivosFijos();
                if (activofijos == null || !activofijos.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron activos fijos.";
                    return NotFound(_response);
                }
                return Ok(activofijos);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener los activos fijos.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener todos los Activos Fijos", Details = ex.Message });
            }
        }

        // GET: api/ActivosFijos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActivosFijos>> GetActivosFijos(int id)
        {
            if(!await _activosFijosRepositorio.ActivosFijosExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Activo fijo no encontrado.";
                return Ok(_response);
            }
            try
            {
                var activosFijos = await _activosFijosRepositorio.GetActivosFijosById(id);
                if(activosFijos != null)
                {
                    _response.Result = activosFijos;
                    _response.DisplayMessage = "Activo fijo encontrado.";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Activo fijo no encontrado.";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al obtener el activo fijo. ");
                return StatusCode(500, _response);
            }
        }

        // PUT: api/ActivosFijos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivosFijos(int id, DtoActivoFijos activosFijosDto)
        {
            if(!await _activosFijosRepositorio.ActivosFijosExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Activo fijo no encontrado.";
                return NotFound(_response);
            }
            try
            {
                var activoFijos = await _activosFijosRepositorio.Update(activosFijosDto);
                _response.Result = activoFijos;
                _response.DisplayMessage = "Activo fijo actualizado correctamente.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.DisplayMessage = "Error al actualizar el activo fijo. ";
                return BadRequest(_response);
            }
        }

        // POST: api/ActivosFijos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ActivosFijos>> PostActivosFijos(DtoActivoFijos activosFijosDto)
        {
            try
            {
                DtoActivoFijos activoFijos = await _activosFijosRepositorio.Create(activosFijosDto);
                return StatusCode(201, new { Message = "Activo fijo creado correctamente.", Result = activoFijos });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el activo fijo.");
                return BadRequest(new { Message = "Error al crear el activo fijo.", Details = ex.Message });
            }
        }

        // DELETE: api/ActivosFijos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivosFijos(int id)
        {
            try
            {
                bool deleted = await _activosFijosRepositorio.DeleteActivosFijos(id);
                if (deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "Activo fijo no encontrado." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el activo fijo.");
                return StatusCode(500, new { Message = "Error al eliminar el activo fijo.", Details = ex.Message });
            }
        }

        private bool ActivosFijosExists(int id)
        {
            return _context.ActivosFijos.Any(e => e.Id == id);
        }
    }
}
