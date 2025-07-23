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
    public class UsuarioSucursalController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UsuarioSucursalController> _logger;
        protected ResponseDto _response;
        private readonly IUsuarioSucursalRepositorio _usuarioSucursalRepositorio;

        public UsuarioSucursalController(ApplicationDbContext context, ILogger<UsuarioSucursalController> logger, IUsuarioSucursalRepositorio usuarioSucursalRepositorio)
        {
            _context = context;
            _logger = logger;
            _response = new ResponseDto();
            _usuarioSucursalRepositorio = usuarioSucursalRepositorio;
        }

        // GET: api/UsuarioSucursal
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioSucursal>>> GetUsuarioSucursal()
        {
            try
            {
                var usuarioSucursal = await _usuarioSucursalRepositorio.GetUsuarioSucursal();
                if (usuarioSucursal == null || !usuarioSucursal.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron registros de UsuarioSucursal.";
                    return NotFound(_response);
                }
                return Ok(usuarioSucursal);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener UsuarioSucursal.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener UsuarioSucursal"});
            }
        }

        // GET: api/UsuarioSucursal/5
        [HttpGet("{usuarioId}/{sucursalId}")]
        public async Task<ActionResult<UsuarioSucursal>> GetUsuarioSucursal(int usuarioId, int sucursalId)
        {
            if(!await _usuarioSucursalRepositorio.UsuarioSucursalExists(usuarioId, sucursalId))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "UsuarioSucursal no encontrado.";
                return NotFound(_response);
            }
            try
            {
                var usuarioSucursal = await _usuarioSucursalRepositorio.GetUsuarioSucursalById(usuarioId, sucursalId);
                if(usuarioSucursal != null)
                {
                    _response.Result = usuarioSucursal;
                    _response.DisplayMessage = "UsuarioSucursal encontrado correctamente. ";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontró UsuarioSucursal. ";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al obtener UsuarioSucursal. ");
                return StatusCode(500, _response);
            }
        }

        // PUT: api/UsuarioSucursal/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{usuarioId}/{sucursalId}")]
        public async Task<IActionResult> PutUsuarioSucursal(int usuarioId, int sucursalId, [FromBody]  DtoUsuarioSucursal usuarioSucursalDto)
        {
            if(!await _usuarioSucursalRepositorio.UsuarioSucursalExists(usuarioId, sucursalId))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "No se encontró UsuarioSucursal. ";
                return NotFound(_response);
            }
            try
            {
                var usuarioSucursal = await _usuarioSucursalRepositorio.Update(usuarioSucursalDto);
                _response.Result = usuarioSucursal;
                _response.DisplayMessage = "UsuarioSucursal actualizado correctamente.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.DisplayMessage = "Error al actualizar UsuarioSucursal.";
                return BadRequest(_response);
            }
        }

        // POST: api/UsuarioSucursal
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UsuarioSucursal>> PostUsuarioSucursal(DtoUsuarioSucursal usuarioSucursalDto)
        {
            try
            {
                DtoUsuarioSucursal usuarioSucursal = await _usuarioSucursalRepositorio.Create(usuarioSucursalDto);
                return StatusCode(201, new { Message = "UsuarioSucursal creada correctamente.", Data = usuarioSucursal });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear UsuarioSucursal.");
                return StatusCode(500, new { Message = "Error al crear UsuarioSucursal.", Details = ex.Message });
            }
        }

        // DELETE: api/UsuarioSucursal/5
        [HttpDelete("{usuarioId}/{sucursalId}")]
        public async Task<IActionResult> DeleteUsuarioSucursal(int usuarioId, int sucursalId)
        {
            try
            {
                bool deleted = await _usuarioSucursalRepositorio.DeleteUsuarioSucursal(usuarioId, sucursalId);
                if(deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "UsuarioSucursal no encontrado." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar UsuarioSucursal.");
                return StatusCode(500, new { Message = "Error al eliminar UsuarioSucursal", Details = ex.Message });
            }
        }
        private bool UsuarioSucursalExists(int id)
        {
            return _context.UsuarioSucursal.Any(e => e.UsuarioId == id);
        }
    }
}
