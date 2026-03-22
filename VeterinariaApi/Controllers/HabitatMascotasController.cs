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
    public class HabitatMascotasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HabitatMascotasController> _logger;
        private readonly IHabitatRepositorio _habitatRepositorio;
        protected ResponseDto _response;

        public HabitatMascotasController(ApplicationDbContext context, IHabitatRepositorio habitatRepositorio, ILogger<HabitatMascotasController> logger)
        {
            _context = context;
            _logger = logger;
            _habitatRepositorio = habitatRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/HabitatMascotas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HabitatMascota>>> GetHabitatMascota()
        {
            try
            {
                var habitat = await _habitatRepositorio.GetHabitat();
                if(habitat == null || !habitat.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron habitats";
                    return NotFound(_response);
                }
                return Ok(habitat);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener los habitats";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener los habitats", Details = ex.Message });
            }
        }

        // GET: api/HabitatMascotas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HabitatMascota>> GetHabitatMascota(int id)
        {
            if(!await _habitatRepositorio.HabitatExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "No se encontro el habitat";
                return Ok(_response);
            }
            try
            {
                var habitat = await _habitatRepositorio.GetHabitatById(id);
                if(habitat != null)
                {
                    _response.Result = false;
                    _response.DisplayMessage = "Habitad encontrada correctamente";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontro el habitat";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al obtener el habitat. ");
                return StatusCode(500, _response);
            }
        }

        // PUT: api/HabitatMascotas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHabitatMascota(int id, DtoHabitatMascota habitatMascotaDto)
        {
            if(!await _habitatRepositorio.HabitatExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Habitat no encontrada. ";
                return NotFound(_response);
            }
            try
            {
                var habitat = await _habitatRepositorio.Update(habitatMascotaDto);
                _response.Result = habitat;
                _response.DisplayMessage = "Habitat actualizada correctamente. ";
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.DisplayMessage = "Error al actualzar Habitat. ";
                return BadRequest(_response);
            }
        }

        // POST: api/HabitatMascotas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HabitatMascota>> PostHabitatMascota(DtoHabitatMascota habitatMascotaDto)
        {
            try
            {
                DtoHabitatMascota habitat = await _habitatRepositorio.Create(habitatMascotaDto);
                return StatusCode(201, new { Message = "Habitat creada correctamente. " });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el Habitat. ");
                return BadRequest(new { Message = "Error al crear el Habitat. ", Details = ex.Message });
            }
        }

        // DELETE: api/HabitatMascotas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHabitatMascota(int id)
        {
            try
            {
                bool delted = await _habitatRepositorio.DeleteHabitat(id);
                if(delted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "Habitat no encontrada. " });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el Habitat. ");
                return StatusCode(500, new { Message = "Error al eliminar el Habitat. ", Details = ex.Message });
            }
        }

        private bool HabitatMascotaExists(int id)
        {
            return _context.HabitatMascota.Any(e => e.Id == id);
        }
    }
}
