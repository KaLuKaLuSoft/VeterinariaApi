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
    public class RazaMascotasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IRazaMascotaRepositorio _razaMascotaRepositorio;
        private readonly ILogger<RazaMascotasController> _logger;
        protected ResponseDto _response;

        public RazaMascotasController(ApplicationDbContext context, ILogger<RazaMascotasController> logger, IRazaMascotaRepositorio razaMascotaRepositorio)
        {
            _context = context;
            _logger = logger;
            _razaMascotaRepositorio = razaMascotaRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/RazaMascotas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RazaMascota>>> GetRazaMascotas()
        {
            try
            {
                var razamascotas = await _razaMascotaRepositorio.GetRazaMascota();
                if(razamascotas == null || !razamascotas.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron raza de mascotas. ";
                    return NotFound(_response);
                }
                return Ok(razamascotas);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener la raza de mascota. ";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener todas las razas de mascotas. ", Details = ex.Message});
            }
        }

        // GET: api/RazaMascotas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RazaMascota>> GetRazaMascota(int id)
        {
            if(!await _razaMascotaRepositorio.RazaMascotaExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "No se encontró la raza de mascota. ";
                return Ok(_response);
            }
            try
            {
                var razamascotas = await _razaMascotaRepositorio.GetRazaMascotaById(id);
                if(razamascotas != null)
                {
                    _response.Result = razamascotas;
                    _response.DisplayMessage = "Raza de mascota encontrada correctamente. ";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontró la raza de mascota. ";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex,"Error al obtener la raza de mascota. ");
                return StatusCode(500, _response);
            }
        }

        // PUT: api/RazaMascotas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRazaMascota(int id, RazaMascota razaMascota)
        {
            if (id != razaMascota.Id)
            {
                return BadRequest();
            }

            _context.Entry(razaMascota).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RazaMascotaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RazaMascotas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RazaMascota>> PostRazaMascota(RazaMascota razaMascota)
        {
            _context.RazaMascotas.Add(razaMascota);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRazaMascota", new { id = razaMascota.Id }, razaMascota);
        }

        // DELETE: api/RazaMascotas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRazaMascota(int id)
        {
            var razaMascota = await _context.RazaMascotas.FindAsync(id);
            if (razaMascota == null)
            {
                return NotFound();
            }

            _context.RazaMascotas.Remove(razaMascota);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RazaMascotaExists(int id)
        {
            return _context.RazaMascotas.Any(e => e.Id == id);
        }
    }
}
