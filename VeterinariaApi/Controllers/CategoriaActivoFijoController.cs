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
    public class CategoriaActivoFijoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoriaActivoFijoRepositorio _categoriaActivoFijoRepositorio;
        private readonly ILogger<CategoriaActivoFijoController> _logger;
        private readonly ResponseDto _response;
        public CategoriaActivoFijoController(ApplicationDbContext context, ILogger<CategoriaActivoFijoController> logger, ICategoriaActivoFijoRepositorio categoriaActivoFijoRepositorio)
        {
            _context = context;
            _logger = logger;
            _categoriaActivoFijoRepositorio = categoriaActivoFijoRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/CategoriaActivoFijo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaActivoFijo>>> GetCategoriaActivoFijo()
        {
            try
            {
                var categorias = await _categoriaActivoFijoRepositorio.GetCategoriaActivoFijO();
                if (categorias == null || !categorias.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron categorías de activo fijo.";
                    return NotFound(_response);
                }
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener las categorías de activo fijo.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener todas las categorías de activo fijo", Details = ex.Message });
            }
        }

        // GET: api/CategoriaActivoFijo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaActivoFijo>> GetCategoriaActivoFijo(int id)
        {
            if(!await _categoriaActivoFijoRepositorio.CategoriaActivoFijoExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Categoría de activo fijo no encontrada.";
                return Ok(_response);
            }
            try
            {
                var categoriaActivoFijo = await _categoriaActivoFijoRepositorio.GetCategoriaActivoFijoById(id);
                if (categoriaActivoFijo == null)
                {
                    _response.Result = categoriaActivoFijo;
                    _response.DisplayMessage = "Categoría de activo fijo encontrada.";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Categoría de activo fijo no encontrada.";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al obtener la categoría de activo fijo. " );
                return StatusCode(500, _response);
            }
        }

        // PUT: api/CategoriaActivoFijo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriaActivoFijo(int id, DtoCategoriaActivoFijo categoriaActivoFijoDto)
        {
            if(!await _categoriaActivoFijoRepositorio.CategoriaActivoFijoExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Categoría de activo fijo no encontrada.";
                return NotFound(_response);
            }
            try
            {
                var categoria = await _categoriaActivoFijoRepositorio.Update(categoriaActivoFijoDto);
                _response.Result = categoria;
                _response.DisplayMessage = "Categoría de activo fijo encontrada.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.DisplayMessage = "Error al actualizar la categoría de activo fijo.";
                return BadRequest(_response);
            }
        }

        // POST: api/CategoriaActivoFijo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoriaActivoFijo>> PostCategoriaActivoFijo(DtoCategoriaActivoFijo categoriaActivoFijoDto)
        {
            try
            {
                DtoCategoriaActivoFijo categorias = await _categoriaActivoFijoRepositorio.Create(categoriaActivoFijoDto);
                return StatusCode(201, new { Message = "Categoría de activo fijo creada correctamente.", Data = categorias });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error al crear la categoría de activo fijo.");
                return BadRequest(new { Message = "Error al crear la categoría de activo fijo", Details = ex.Message });
            }
        }

        // DELETE: api/CategoriaActivoFijo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoriaActivoFijo(int id)
        {
            try
            {
                bool deleted = await _categoriaActivoFijoRepositorio.DeleteCategoriaActivoFijo(id);
                if (deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "Categoría de activo fijo no encontrada." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la categoría de activo fijo.");
                return BadRequest(new { Message = "Error al eliminar la categoría de activo fijo", Details = ex.Message });
            }
        }

        private bool CategoriaActivoFijoExists(int id)
        {
            return _context.CategoriaActivoFijo.Any(e => e.Id == id);
        }
    }
}
