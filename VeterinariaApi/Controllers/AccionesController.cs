using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;
using VeterinariaApi.Models;

namespace VeterinariaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccionesRepositorio _accionesRepositorio;
        private readonly ILogger<AccionesController> _logger;
        protected ResponseDto _response;
        public AccionesController(ApplicationDbContext context, ILogger<AccionesController> logger, IAccionesRepositorio accionesRepositorio)
        {
            _context = context;
            _logger = logger;
            _accionesRepositorio = accionesRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/Acciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Acciones>>> GetAcciones()
        {
            try
            {
                var acciones = await _accionesRepositorio.GetAcciones();
                if(acciones == null || !acciones.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron acciones.";
                    return NotFound(_response);
                }
                return Ok(acciones);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener las acciones.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener todas las Acciones", Details = ex.Message });
            }
        }

        // GET: api/Acciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Acciones>> GetAcciones(int id)
        {
            if(!await _accionesRepositorio.AccionesExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Acción no encontrada.";
                return Ok(_response);
            }
            try
            {
                var acciones = await _accionesRepositorio.GetAccionesById(id);
                if(acciones != null)
                {
                    _response.Result = acciones;
                    _response.DisplayMessage = "Acciones encontrada correctamente.";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Acción no encontrada.";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex,"Error al obtener la acción.");
                return StatusCode(500, _response);
            }
        }

        // PUT: api/Acciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAcciones(int id, DtoAcciones accionesDto)
        {
            if(!await _accionesRepositorio.AccionesExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Acción no encontrada.";
                return NotFound(_response);
            }
            try
            {
                var acciones = await _accionesRepositorio.Update(accionesDto);
                _response.Result = acciones;
                _response.DisplayMessage = "Accion actualizada correctamente.";
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al actualizar la acción.";
                return BadRequest(_response);
            }
        }

        // POST: api/Acciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Acciones>> PostAcciones(DtoAcciones accionesDto)
        {
            try
            {
                DtoAcciones acciones = await _accionesRepositorio.Create(accionesDto);
                return StatusCode(201, new { message = "Acción creada correctamente.", Data = acciones });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la acción.");
                return BadRequest(new { Message = "Error al crear la acción", Details = ex.Message });
            }
        }

        // DELETE: api/Acciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAcciones(int id)
        {
            try
            {
                bool deleted = await _accionesRepositorio.DeleteAcciones(id);
                if(deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "Acción no encontrada." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la acción.");
                return StatusCode(500, new { Message = "Error al eliminar la acción", Details = ex.Message });
            }
        }

        private bool AccionesExists(int id)
        {
            return _context.Acciones.Any(e => e.Id == id);
        }
    }
}
