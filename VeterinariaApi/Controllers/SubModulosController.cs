using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;
using VeterinariaApi.Models;

namespace VeterinariaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubModulosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ISubModuloRepositorio _subModuloRepositorio;
        private readonly ILogger<SubModulosController> _logger;
        protected ResponseDto _response;
        public SubModulosController(ApplicationDbContext context, ILogger<SubModulosController> logger, ISubModuloRepositorio subModuloRepositorio)
        {
            _context = context;
            _logger = logger;
            _subModuloRepositorio = subModuloRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/SubModulos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubModulo>>> GetSubModulos()
        {
            try
            {
                var subModulos = await _subModuloRepositorio.GetSubModulo();
                if (subModulos == null || !subModulos.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron submódulos.";
                    return NotFound(_response);
                }
                return Ok(subModulos);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener los submódulos.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener todos los submódulos", Details = ex.Message });
            }
        }

        // GET: api/SubModulos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubModulo>> GetSubModulo(int id)
        {
            if(!await _subModuloRepositorio.SubModuloExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Submódulo no encontrado.";
                return NotFound(_response);
            }
            try
            {
                var subModulo = await _subModuloRepositorio.GetSubModuloById(id);
                if (subModulo != null)
                {
                    _response.Result = subModulo;
                    _response.DisplayMessage = "Submódulo encontrado.";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Submódulo no encontrado.";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al obtener el submódulo");
                return StatusCode(500, _response);
            }
        }

        // PUT: api/SubModulos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubModulo(int id, DtoSubModulo subModuloDto)
        {
            if(!await _subModuloRepositorio.SubModuloExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Submódulo no encontrado.";
                return NotFound(_response);
            }
            try
            {
                var submodulo = await _subModuloRepositorio.Update(subModuloDto);
                _response.Result = submodulo;
                _response.DisplayMessage = "Submódulo actualizado correctamente.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al actualizar el submódulo";
                _response.ErrorMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
        }

        // POST: api/SubModulos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubModulo>> PostSubModulo(DtoSubModulo subModuloDto)
        {
            try
            {
                DtoSubModulo submodulo = await _subModuloRepositorio.Create(subModuloDto);
                return StatusCode(201, new {Message = "Submódulo creado correctamente.", Data = submodulo });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el submódulo");
                return BadRequest(new { Message = "Error al crear el submódulo", Details = ex.Message });
            }
        }

        // DELETE: api/SubModulos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubModulo(int id)
        {
            try
            {
                bool deleted = await _subModuloRepositorio.DeleteSubModulo(id);
                if(deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "Submódulo no encontrado." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el submódulo");
                return StatusCode(600, new { Message = "Error al eliminar el submódulo", Details = ex.Message });
            }
        }

        private bool SubModuloExists(int id)
        {
            return _context.SubModulos.Any(e => e.Id == id);
        }
    }
}
