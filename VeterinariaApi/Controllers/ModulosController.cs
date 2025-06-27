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
    public class ModulosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ModulosController> _logger;
        private readonly IModuloRepositorio _moduloRepositorio;
        protected ResponseDto _response;

        public ModulosController(ApplicationDbContext context, ILogger<ModulosController> logger, IModuloRepositorio moduloRepositorio)
        {
            _context = context;
            _logger = logger;
            _moduloRepositorio = moduloRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/Modulos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Modulo>>> GetModulos()
        {
            try
            {
                var modulo = await _moduloRepositorio.GetModulo();

                if(modulo == null || !modulo.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron módulos.";
                    return NotFound(_response);
                }
                return Ok(modulo);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener los modulos";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, _response);
            }
        }

        // GET: api/Modulos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Modulo>> GetModulo(int id)
        {
            if(!await _moduloRepositorio.ModuloExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Módulo no encontrado.";
                return NotFound(_response);
            }
            try
            {
                var modulo = await _moduloRepositorio.GetModuloById(id);
                if(modulo != null)
                {
                    _response.Result = modulo;
                    _response.DisplayMessage = "Modulo encontrado correctamente";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Módulo no encontrado.";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener el módulo.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, _response);
            }
        }

        // PUT: api/Modulos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModulo(int id, DtoModulo moduloDto)
        {
            if(!await _moduloRepositorio.ModuloExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Módulo no encontrado.";
                return NotFound(_response);
            }
            try
            {
                var modulos = await _moduloRepositorio.Update(moduloDto);
                _response.Result = modulos;
                _response.DisplayMessage = "Módulo actualizado correctamente.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al actualizar el módulo.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
        }

        // POST: api/Modulos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Modulo>> PostModulo(DtoModulo moduloDto)
        {
            try
            {
                DtoModulo modulos = await _moduloRepositorio.Create(moduloDto);
                return StatusCode(201, new { Message = "Módulo creado correctamente", Data = modulos });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el módulo.");
                return BadRequest(new { Message = "Error al crear el módulo", Details = ex.Message });
            }
        }

        // DELETE: api/Modulos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModulo(int id)
        {
            try
            {
                bool deleted = await _moduloRepositorio.DeleteModulo(id);
                if (deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "Módulo no encontrado." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el módulo");
                return StatusCode(500, new { Message = "Error al eliminar el módulo", Details = ex.Message });
            }
        }

        private bool ModuloExists(int id)
        {
            return _context.Modulos.Any(e => e.Id == id);
        }
    }
}
