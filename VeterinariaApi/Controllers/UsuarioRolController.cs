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
    public class UsuarioRolController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UsuarioRolController> _logger;
        protected ResponseDto _response;
        private readonly IUsuarioRolRepositorio _usuariorolRepositorio;

        public UsuarioRolController(ApplicationDbContext context, ILogger<UsuarioRolController> logger, IUsuarioRolRepositorio usuarioRolRepositorio)
        {
            _context = context;
            _logger = logger;
            _response = new ResponseDto();
            _usuariorolRepositorio = usuarioRolRepositorio;
        }

        // GET: api/UsuarioRol
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioRol>>> GetUsuarioRol()
        {
            try
            {
                var usuarioRol = await _usuariorolRepositorio.GetUsuarioRol();
                if(usuarioRol == null || !usuarioRol.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron UsuarioRol. ";
                    return NotFound(_response);
                }
                return Ok(usuarioRol);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener UsuarioRol. ";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener UsuarioRol. " });
            }
        }

        // GET: api/UsuarioRol/5
        [HttpGet("{usuarioId}/{rolId}")]
        public async Task<ActionResult<UsuarioRol>> GetUsuarioRol(int usuarioId, int rolId)
        {
            if(!await _usuariorolRepositorio.UsuarioRolExists(usuarioId, rolId))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "No se encontró UusarioRol. ";
                return NotFound(_response);
            }
            try
            {
                var usuarioRol = await _usuariorolRepositorio.GetUsuarioRolById(usuarioId, rolId);
                if(usuarioRol != null)
                {
                    _response.Result = usuarioRol;
                    _response.DisplayMessage = "UsuarioRol encontrado correctamente. ";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontró UusarioRol. ";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al obtener UsuarioRol.");
                return StatusCode(500, _response);
            }
        }

        // PUT: api/UsuarioRol/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{usuarioId}/{rolId}")]
        public async Task<IActionResult> PutUsuarioRol(int usuarioId, int rolId, [FromBody] DtoUsuarioRol usuarioRolDto)
        {
            if(!await _usuariorolRepositorio.UsuarioRolExists(usuarioId, rolId))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "No se encontró UusarioRol. ";
                return NotFound(_response) ;
            }
            try
            {
                var usuarioRol = await _usuariorolRepositorio.Update(usuarioRolDto);
                _response.Result = usuarioRol;
                _response.DisplayMessage = "UsuarioRol actualizado correctamente. ";
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al actualizar UsuarioRol. ";
                return StatusCode(500, _response);
            }
        }

        // POST: api/UsuarioRol
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UsuarioRol>> PostUsuarioRol(DtoUsuarioRol usuarioRolDto)
        {
            try
            {
                DtoUsuarioRol usuarioRol = await _usuariorolRepositorio.Create(usuarioRolDto);
                return StatusCode(201, new { Message = "UsuarioRol creada correctamente. " });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el UsuarioRol. ");
                return StatusCode(500, new { Message = "Error al crear UsuarioRol"});
            }
        }

        // DELETE: api/UsuarioRol/5
        [HttpDelete("{usuarioId}/{rolId}")]
        public async Task<IActionResult> DeleteUsuarioRol(int usuarioId, int rolId)
        {
            try
            {
                bool deleted = await _usuariorolRepositorio.DeleteUsuarioRol(usuarioId, rolId);
                if (deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "No se encontró UsuarioRol. " });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar UsuarioRol. ");
                return StatusCode(500, new { Message = "Error al eliminar UsuarioRol. ", Details = ex.Message });
            }
        }

        private bool UsuarioRolExists(int id)
        {
            return _context.UsuarioRol.Any(e => e.UsuarioId == id);
        }
    }
}
