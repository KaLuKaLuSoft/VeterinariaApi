using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class ProcedenciaMascotasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AccionesController> _logger;
        private readonly IProcedenciaMascotaRepositorio _procedenciaMascotaRepositorio;
        protected ResponseDto _response;
        public ProcedenciaMascotasController(ApplicationDbContext context, ILogger<AccionesController>logger, IProcedenciaMascotaRepositorio procedenciaMascotaRepositorio)
        {
            _context = context;
            _logger = logger;
            _procedenciaMascotaRepositorio = procedenciaMascotaRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/ProcedenciaMascotas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcedenciaMascota>>> GetProcedenciaMascota()
        {
            try
            {
                var procedencia = await _procedenciaMascotaRepositorio.GetProcedenciaMascota();
                if (procedencia == null || !procedencia.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron Procedencia de Mascota. ";
                    return NotFound(_response);
                }
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener todas las Procedencia de Mascota. ";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return StatusCode(500, new { Message = "Error al obtener todas las Procedencia de Mascota. ", Details = ex.Message });
            }
        }

        // GET: api/ProcedenciaMascotas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcedenciaMascota>> GetProcedenciaMascota(int id)
        {
            if(!await _procedenciaMascotaRepositorio.ProcedenciaMascotaExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "No se encontro la Procedencia de Mascota. ";
                return Ok(_response);
            }
            try
            {
                var procedencia = await _procedenciaMascotaRepositorio.GetProcedenciaMascotaById(id);
                if(procedencia != null)
                {
                    _response.Result = procedencia;
                    _response.DisplayMessage = "Procedencia de Mascota obtenida con exito. ";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontro la Procedencia de Mascota. ";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al obtener la Procedencia de Mascota. ");
                return StatusCode(500, _response);
            }
        }

        // PUT: api/ProcedenciaMascotas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProcedenciaMascota(int id, DtoProcedenciaMascota procedenciaMascotaDto)
        {
            if(!await _procedenciaMascotaRepositorio.ProcedenciaMascotaExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "No se encontro la Procedencia de Mascota. ";
                return NotFound(_response);
            }
            try
            {
                var procedencia = await _procedenciaMascotaRepositorio.Update(procedenciaMascotaDto);
                _response.Result = procedencia;
                _response.DisplayMessage = "Procedencia de Mascota actualizada con exito. ";
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.DisplayMessage = "Error al actualizar la Procedencia de Mascota. ";
                return BadRequest(_response);
            }
        }

        // POST: api/ProcedenciaMascotas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProcedenciaMascota>> PostProcedenciaMascota(DtoProcedenciaMascota procedenciaMascotaDto)
        {
            try
            {
                DtoProcedenciaMascota procedencia = await _procedenciaMascotaRepositorio.Create(procedenciaMascotaDto);
                return StatusCode(201, new { Message = "Procedencia de Mascota creada con exito. ", Data = procedencia });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la Procedencia de Mascota. ");
                return BadRequest(new { Message = "Error al crear la Procedencia de Mascota. ", Details = ex.Message });
            }
        }

        // DELETE: api/ProcedenciaMascotas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcedenciaMascota(int id)
        {
            try
            {
                bool deleted = await _procedenciaMascotaRepositorio.DeleteProcedenciaMascota(id);
                if(deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "No se encontro la Procedencia de Mascota. " });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la Procedencia de Mascota. ");
                return StatusCode(500, new { Message = "Error al eliminar la Procedencia de Mascota. ", Details = ex.Message });
            }
        }

        private bool ProcedenciaMascotaExists(int id)
        {
            return _context.ProcedenciaMascota.Any(e => e.Id == id);
        }
    }
}
