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
    public class SucursalesController : ControllerBase
    {
        private readonly ISucursalesRepositorio _sucursalesRepositorio;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SucursalesController> _logger;
        protected ResponseDto _response;

        public SucursalesController(ApplicationDbContext context, ILogger<SucursalesController> logger, ISucursalesRepositorio sucursalesRepositorio)
        {
            _context = context;
            _logger = logger;
            _sucursalesRepositorio = sucursalesRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/Sucursales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sucursales>>> GetSucursales()
        {
            try
            {
                var sucursales = await _sucursalesRepositorio.GetSucursales();
                if (sucursales == null || !sucursales.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron sucursales.";
                    return NotFound(_response);
                }
                return Ok(sucursales);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener las sucursales.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener todos las sucursales", Details = ex.Message });
            }
        }

        // GET: api/Sucursales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sucursales>> GetSucursales(int id)
        {
            if(!await _sucursalesRepositorio.SucursalesExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Sucursal no encontrada.";
                return NotFound(_response);
            }
            try
            {
                var sucursales = await _sucursalesRepositorio.GetSucursalesById(id);
                if (sucursales != null)
                {
                    _response.Result = sucursales;
                    _response.DisplayMessage = "Sucursal no encontrada.";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Sucursal encontrada.";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al obtener la sucursal.");
                return StatusCode(500, _response);
            }
        }

        // PUT: api/Sucursales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSucursales(int id, DtoSucursales sucursalesDto)
        {
            if(!await _sucursalesRepositorio.SucursalesExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Sucursal no encontrada.";
                return NotFound(_response);
            }
            try
            {
                var sucursales = await _sucursalesRepositorio.Update(sucursalesDto);
                _response.Result = sucursales;
                _response.DisplayMessage = "Sucursal actualizada correctamente.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al actualizar la sucursal.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
        }

        // POST: api/Sucursales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sucursales>> PostSucursales(DtoSucursales sucursalesDto)
        {
            try
            {
                DtoSucursales sucursales = await _sucursalesRepositorio.Create(sucursalesDto);
                return StatusCode(201, new { Message = "Sucursal creada correctamente.", Data = sucursales });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la sucursal");
                return BadRequest(new { Message = "Error al crear la sucursal.", Details = ex.Message });
            }
        }

        // DELETE: api/Sucursales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSucursales(int id)
        {
            try
            {
                bool deleted = await _sucursalesRepositorio.DeleteSucursales(id);
                if (deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "Sucursal no encontrada." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la sucursal");
                return StatusCode(500, new { Message = "Error al eliminar la sucursal.", Details = ex.Message });
            }
        }

        private bool SucursalesExists(int id)
        {
            return _context.Sucursales.Any(e => e.Id == id);
        }
    }
}
