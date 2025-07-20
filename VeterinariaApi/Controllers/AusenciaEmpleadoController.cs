using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
    public class AusenciaEmpleadoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AusenciaEmpleadoController> _logger;
        protected ResponseDto _response;
        public IAusenciaEmpleadoRepositorio _ausenciaEmpleadoRepositorio;

        public AusenciaEmpleadoController(ApplicationDbContext context, ILogger<AusenciaEmpleadoController> logger, IAusenciaEmpleadoRepositorio ausenciaEmpleadoRepositorio)
        {
            _context = context;
            _logger = logger;
            _ausenciaEmpleadoRepositorio = ausenciaEmpleadoRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/AusenciaEmpleado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AusenciaEmpleado>>> GetAusenciaEmpleado()
        {
            try
            {
                var ausenciaEmpleados = await _ausenciaEmpleadoRepositorio.GetAusenciaEmpleado();
                if (ausenciaEmpleados == null || !ausenciaEmpleados.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron ausencias de empleados.";
                    return NotFound(_response);
                }
                return Ok(ausenciaEmpleados);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener las ausencias de empleados.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener las ausencias de empleados", Details = ex.Message });
            }
        }

        // GET: api/AusenciaEmpleado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AusenciaEmpleado>> GetAusenciaEmpleado(int id)
        {
            if(!await _ausenciaEmpleadoRepositorio.AusenciaEmpleadoExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Ausencia de empleado no encontrada.";
                return NotFound(_response);
            }
            try
            {
                var ausenciaEmpleado = await _ausenciaEmpleadoRepositorio.GetAusenciaEmpleadoById(id);
                if (ausenciaEmpleado != null)
                {
                    _response.Result = ausenciaEmpleado;
                    _response.DisplayMessage = "Ausencia de empleado encontrada.";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Ausencia de empleado no encontrada.";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener la ausencia de empleado.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener la ausencia de empleado", Details = ex.Message });
            }
        }

        // PUT: api/AusenciaEmpleado/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAusenciaEmpleado(int id, DtoAusenciaEmpleado ausenciaEmpleadoDto)
        {
            if(!await _ausenciaEmpleadoRepositorio.AusenciaEmpleadoExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Ausencia de empleado no encontrada.";
                return NotFound(_response);
            }
            try
            {
                var ausenciaEmpleado = await _ausenciaEmpleadoRepositorio.Update(ausenciaEmpleadoDto);
                _response.Result = ausenciaEmpleado;
                _response.DisplayMessage = "Ausencia de empleado actualizada correctamente.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.DisplayMessage = "Error al actualizar la ausencia de empleado.";
                return BadRequest(_response);
            }
        }

        // POST: api/AusenciaEmpleado
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AusenciaEmpleado>> PostAusenciaEmpleado(DtoAusenciaEmpleado ausenciaEmpleadoDto)
        {
            try
            {
                DtoAusenciaEmpleado nuevaAusencia = await _ausenciaEmpleadoRepositorio.Create(ausenciaEmpleadoDto);
                return StatusCode(201, new { message = "Ausencia de empleado creada correctamente", Data = nuevaAusencia });
            }    
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la ausencia de empleado.");
                return BadRequest(new {
                    Message = "Error al crear la ausencia de empleado: ", Details = ex.Message });
            }
        }

        // DELETE: api/AusenciaEmpleado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAusenciaEmpleado(int id)
        {
            var ausenciaEmpleado = await _context.AusenciaEmpleado.FindAsync(id);
            if (ausenciaEmpleado == null)
            {
                return NotFound();
            }

            _context.AusenciaEmpleado.Remove(ausenciaEmpleado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AusenciaEmpleadoExists(int id)
        {
            return _context.AusenciaEmpleado.Any(e => e.Id == id);
        }
    }
}
