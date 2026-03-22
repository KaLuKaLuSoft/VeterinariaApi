using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class DueñosController : ControllerBase
    {
        private readonly IDueñosRepositorio _dueñosRepositorio;
        private readonly ILogger<DueñosController> _logger;
        private readonly ApplicationDbContext _context;
        protected ResponseDto _response;

        public DueñosController(ApplicationDbContext context, ILogger<DueñosController> logger, IDueñosRepositorio dueñosRepositorio)
        {
            _context = context;
            _dueñosRepositorio = dueñosRepositorio;
            _logger = logger;
            _response = new ResponseDto();
        }

        // GET: api/Dueños
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dueños>>> GetDueños()
        {
            try
            {
                var idEmpresaClaim = User.FindFirst("IdEmpresa")?.Value;

                if(string.IsNullOrEmpty(idEmpresaClaim))
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontró la empresa en el token de seguridad. ";
                    return Unauthorized(_response);
                }

                int idEmpresa = int.Parse(idEmpresaClaim);

                var dueños = await _dueñosRepositorio.GetDueños(idEmpresa);

                if(dueños == null || !dueños.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontrarons dueños para esta empresa.";
                    return NotFound(_response);
                }

                _response.IsSuccess = true;
                _response.Result = dueños;
                _response.DisplayMessage = "Lista de dueños obtenidos con éxito. ";

                return Ok(dueños);
            }
            catch   (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener los dueños. ";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, _response);
            }
        }

        // GET: api/Dueños/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dueños>> GetDueños(int id, int idEmpresa)
        {
            if(!await _dueñosRepositorio.DueñosExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Dueños no encontrado. ";
                return Ok(_response);
            }
            try
            {
                var dueños = await _dueñosRepositorio.GetDueñosById(id, idEmpresa);
                if(dueños != null)
                {
                    _response.Result = dueños;
                    _response.DisplayMessage = "Dueño encontrado correctamente. ";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Dueño no encontrado. ";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                _logger.LogError(ex, "Error al obtener el cliente. ");
                return StatusCode(500, _response);
            }
        }

        // PUT: api/Dueños/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDueños(int id, DtoDueños dueñosDto)
        {
            if(!await _dueñosRepositorio.DueñosExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Dueño no encontrado. ";
                return NotFound(_response);
            }
            try
            {
                var dueños = await _dueñosRepositorio.Update(dueñosDto);
                _response.Result = dueños;
                _response.DisplayMessage = "Dueño actualizado correctamente. ";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.DisplayMessage = "Error al actualizar al dueño. ";
                return BadRequest(_response);
            }
        }

        // POST: api/Dueños
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dueños>> PostDueños(DtoDueños dueñosDto)
        {
            try
            {
                DtoDueños dueños = await _dueñosRepositorio.Create(dueñosDto);
                return StatusCode(201, new { Message = "Dueño creado correctamente. " });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error al crear al dueño. ");
                return BadRequest(new { Message = "Error al crear al dueño. ", Details = ex.Message });
            }
        }

        // DELETE: api/Dueños/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDueños(int id)
        {
            try
            {
                var idEmpresaClaim = User.FindFirst("IdEmpresa")?.Value;
                if(string.IsNullOrEmpty(idEmpresaClaim))return Unauthorized();

                int idEmpresa = int.Parse(idEmpresaClaim);

                bool deleted = await _dueñosRepositorio.DeleteDueños(id, idEmpresa);

                if(deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "Dueño no encontrado o no pertenece a su empresa. " });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al eliminar al dueño. ", Details = ex.Message });
            }
        }

        private bool DueñosExists(int id)
        {
            return _context.Dueños.Any(e => e.Id == id);
        }
    }
}
