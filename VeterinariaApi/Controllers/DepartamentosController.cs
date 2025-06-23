using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinariaApi.Data;
using VeterinariaApi.Interface;
using VeterinariaApi.Models;
using VeterinariaApi.Dto;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
namespace VeterinariaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        private readonly IDepartamentosRepositorio _departamentosRepositorio;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DepartamentosController> _logger;
        protected ResponseDto _response;

        public DepartamentosController(ApplicationDbContext context, ILogger<DepartamentosController> logger, IDepartamentosRepositorio departamentosRepositorio)
        {
            _context = context;
            _logger = logger;
            _departamentosRepositorio = departamentosRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/Departamentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Departamentos>>> GetDepartamentos()
        {
            try
            {
                var departamentos = await _departamentosRepositorio.GetDepartamentos();
                if (departamentos == null || !departamentos.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron departamentos.";
                    return NotFound(_response);
                }
                return Ok(departamentos);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener los departamentos.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, _response);
            }
        }
        // GET: api/Departamentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Departamentos>> GetDepartamentos(int id)
        {
            if(!await _departamentosRepositorio.DeapartamentosExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Departamento no encontrado.";
                return NotFound(_response);
            }
            try
            {
                var departamento = await _departamentosRepositorio.GetDepartamentosById(id);
                if (departamento != null)
                {
                    _response.Result = departamento;
                    _response.DisplayMessage = "Departamento encontrado correctamente.";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Departamento no encontrado.";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al obtener el departamento con ID");
                return StatusCode(500, _response);
            }
        }
        // PUT: api/Departamentos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartamentos(int id, DtoDepartamentos departamentosDto)
        {
            if(!await _departamentosRepositorio.DeapartamentosExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Departamento no encontrado.";
                return NotFound(_response);
            }
            try
            {
                var departamentos = await _departamentosRepositorio.Update(departamentosDto);
                _response.Result = departamentos;
                _response.DisplayMessage = "Departamento actualizado correctamente.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al actualizar el departamento.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, _response);
            }
        }

        // POST: api/Departamentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Departamentos>> PostDepartamentos(DtoDepartamentos departamentosDto)
        {
            try
            {
                DtoDepartamentos departamentos = await _departamentosRepositorio.Create(departamentosDto);
                return StatusCode(201, new { Message = "Departamento creado correctamente.", Data = departamentos });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el departamento.");
                return BadRequest(new { Message = "Error al crear el departamento.", Details = ex.Message });
            }
        }

        // DELETE: api/Departamentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartamentos(int id)
        {
            try
            {
                bool deleted = await _departamentosRepositorio.DeleteDepartamentos(id);
                if (deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "Departamento no encontrado." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el departamento.");
                return StatusCode(500, new { Message = "Error al eliminar el departamento.", Details = ex.Message });
            }
        }

        private bool DepartamentosExists(int id)
        {
            return _context.Departamentos.Any(e => e.Id == id);
        }
    }
}
