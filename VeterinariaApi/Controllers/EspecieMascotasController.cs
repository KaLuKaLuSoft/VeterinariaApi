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
    public class EspecieMascotasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EspecieMascotasController> _logger;
        private readonly IEspecieMascotaRepositorio _especieMascotaRepositorio;
        protected ResponseDto _response;

        public EspecieMascotasController(ApplicationDbContext context, ILogger<EspecieMascotasController> logger, IEspecieMascotaRepositorio especieMascotaRepositorio)
        {
            _context = context;
            _logger = logger;
            _especieMascotaRepositorio = especieMascotaRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/EspecieMascotas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EspecieMascota>>> GetEspecieMascotas()
        {
            try
            {
                var especiemascotas = await _especieMascotaRepositorio.GetEspecieMascota();
                if(especiemascotas == null || !especiemascotas.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron especies de mascotas.";
                    return NotFound(_response);
                }
                return Ok(especiemascotas);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Ocurrió un error al obtener las especies de mascotas.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener todas las EspecieMascotas", Details = ex.Message });
            }
        }

        // GET: api/EspecieMascotas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EspecieMascota>> GetEspecieMascota(int id)
        {
            if(!await _especieMascotaRepositorio.EspecieMascotaExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "No se encontró la especie.";
                return Ok(_response);
            }
            try
            {
                var especiemascotas = await _especieMascotaRepositorio.GetEspecieMascotaById(id);
                if(especiemascotas != null)
                {
                    _response.Result = especiemascotas;
                    _response.DisplayMessage = "Especie encontrada correctamente.";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontró la especie.";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex,"Error al obtener la especie de mascota.");
                return StatusCode(500, _response);
            }
        }

        // PUT: api/EspecieMascotas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEspecieMascota(int id, DtoEspecieMascota especieMascotaDto)
        {
            if(!await _especieMascotaRepositorio.EspecieMascotaExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "No se encontró la especie.";
                return NotFound(_response);
            }
            try
            {
                var especieMascota = await _especieMascotaRepositorio.Update(especieMascotaDto);
                _response.Result = especieMascota;
                _response.DisplayMessage = "Especie actualizada correctamente.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex,"Error al actualizar la especie de mascota.");
                return BadRequest(_response);
            }
        }

        // POST: api/EspecieMascotas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EspecieMascota>> PostEspecieMascota(DtoEspecieMascota especieMascotaDto)
        {
            try
            {
                DtoEspecieMascota especieMascota = await _especieMascotaRepositorio.Create(especieMascotaDto);
                return StatusCode(201, new { Message = "Especie de mascota creada exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error al crear la especie de mascota.");
                return BadRequest(new { Message = "Error al crear la especie de mascota", Details = ex.Message });
            }
        }

        // DELETE: api/EspecieMascotas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspecieMascota(int id)
        {
            var especieMascota = await _context.EspecieMascotas.FindAsync(id);
            if (especieMascota == null)
            {
                return NotFound();
            }

            _context.EspecieMascotas.Remove(especieMascota);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EspecieMascotaExists(int id)
        {
            return _context.EspecieMascotas.Any(e => e.Id == id);
        }
    }
}
