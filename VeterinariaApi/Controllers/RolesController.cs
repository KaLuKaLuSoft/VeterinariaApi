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
    public class RolesController : ControllerBase
    {
        private readonly IRolesRepositorio _rolesRepositorio;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RolesController> _logger;
        protected ResponseDto _response;

        public RolesController(ApplicationDbContext context, ILogger<RolesController> logger, IRolesRepositorio rolesRepositorio)
        {
            _context = context;
            _logger = logger;
            _rolesRepositorio = rolesRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roles>>> GetRoles()
        {
            try
            {
                var roles = await _rolesRepositorio.GetRoles();
                if (roles == null || !roles.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron roles.";
                    return NotFound(_response);
                }
                return Ok(roles);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener los roles.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, _response);
            }
        }
        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Roles>> GetRoles(int id)
        {
            if(!await _rolesRepositorio.RolesExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Rol no encontrado.";
                return NotFound(_response);
            }
            try
            {
                var roles = await _rolesRepositorio.GetRolesById(id);
                if (roles != null)
                {
                    _response.Result = roles;
                    _response.DisplayMessage = "Rol encontrado.";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Rol encontrado.";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al obtener el rol con ID");
                return StatusCode(500, _response);
            }
        }
        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoles(int id, DtoRoles rolesDto)
        {
            if(!await _rolesRepositorio.RolesExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Rol no encontrado.";
                return NotFound(_response);
            }
            try
            {
                var roles = await _rolesRepositorio.Update(rolesDto);
                _response.Result = roles;
                _response.DisplayMessage = "Rol actualizado correctamente.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al actualizar el rol.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, _response);
            }
        }

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Roles>> PostRoles(DtoRoles rolesDto)
        {
            try
            {
                DtoRoles roles = await _rolesRepositorio.Create(rolesDto);
                return StatusCode(201, new { Message = "Rol creado correctamente.", Data = roles });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el rol.");
                return BadRequest(new {Message = "Error al crear el rol.", Details = ex.Message});
            }
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoles(int id)
        {
            try
            {
                bool deleted = await _rolesRepositorio.DeleteRoles(id);
                if (deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "Rol no encontrado." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el rol");
                return StatusCode(500, new { Message = "Error al eliminar el rol.", Details = ex.Message });
            }
        }

        private bool RolesExists(int id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}
