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
    public class ConvivenciaMascotasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ConvivenciaMascotasController> _logger;
        private readonly IConvivenciaMascotaRepositorio _convivenciaMascotaRepositorio;
        protected ResponseDto _response;
        public ConvivenciaMascotasController(ApplicationDbContext context, ILogger<ConvivenciaMascotasController> logger, IConvivenciaMascotaRepositorio convivenciaMascotaRepositorio)
        {
            _context = context;
            _logger = logger;
            _convivenciaMascotaRepositorio = convivenciaMascotaRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/ConvivenciaMascotas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConvivenciaMascota>>> GetConvivenciaMascota()
        {
            try
            {
                var convivenciamascotas = await _convivenciaMascotaRepositorio.GetConvivenciaMascota();
                if(convivenciamascotas == null || !convivenciamascotas.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron registros de convivencia. ";
                    return NotFound(_response);
                }
                return Ok(convivenciamascotas);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener los registros de convivencia. ";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return StatusCode(500, new { Message = "Error al obtener todos los registros de convivencia. ", Details = ex.Message });
            }
        }

        // GET: api/ConvivenciaMascotas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConvivenciaMascota>> GetConvivenciaMascota(int id)
        {
            if(!await _convivenciaMascotaRepositorio.ConvivenciaMascotaExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "No se encontró el registro de convivencia. ";
                return Ok(_response);
            }
            try
            {
                var convivenciaMascota = await _convivenciaMascotaRepositorio.GetConvivenciaMascotaById(id);
                if (convivenciaMascota != null)
                {
                    _response.Result = convivenciaMascota;
                    _response.DisplayMessage = "Se encontró el registro de convivencia. ";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontró el registro de convivencia. ";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al obtener el registro de convivencia. ");
                return StatusCode(500, _response);
             }
        }

        // PUT: api/ConvivenciaMascotas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConvivenciaMascota(int id, DtoConvivenciaMascota convivenciaMascotaDto)
        {
            if(!await _convivenciaMascotaRepositorio.ConvivenciaMascotaExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Convivencia de Mascota no encontrada. ";
                return NotFound(_response);
            }
            try
            {
                var convivencia = await _convivenciaMascotaRepositorio.Update(convivenciaMascotaDto);
                _response.Result = convivencia;
                _response.DisplayMessage = "Convivencia de Mascota actualizada correctamente. ";
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.DisplayMessage = "Error al actualizar la Convivencia de Mascota. ";
                return BadRequest(_response);
            }
        }

        // POST: api/ConvivenciaMascotas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ConvivenciaMascota>> PostConvivenciaMascota(DtoConvivenciaMascota convivenciaMascotaDto)
        {
            try
            {
                DtoConvivenciaMascota convivencia = await _convivenciaMascotaRepositorio.Create(convivenciaMascotaDto);
                return StatusCode(201, new { Message = "Convivencia de Mascota creada correctamente. ", Data = convivencia });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la Convivencia de Mascota. ");
                return BadRequest(new { Message = "Error al crear la Convivencia de Mascota. ", Details = ex.Message });
            }
        }

        // DELETE: api/ConvivenciaMascotas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConvivenciaMascota(int id)
        {
            try
            {
                bool deleted = await _convivenciaMascotaRepositorio.DeleteConvivenciaMascota(id);
                if(deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "No se encontró la Convivencia de Mascota para eliminar. " });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la Convivencia de Mascota. ");
                return BadRequest(new { Message = "Error al eliminar la Convivencia de Mascota. ", Details = ex.Message });
            }
        }

        private bool ConvivenciaMascotaExists(int id)
        {
            return _context.ConvivenciaMascota.Any(e => e.Id == id);
        }
    }
}
