using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;
using VeterinariaApi.Models;

namespace VeterinariaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisesController : ControllerBase
    {
        private readonly IPaisesRepositorio _paisesService;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PaisesController> _logger;
        protected ResponseDto _response;
        public PaisesController(ApplicationDbContext context, ILogger<PaisesController> logger,IPaisesRepositorio paisesService )
        {
            _context = context;
            _logger = logger;
            _paisesService = paisesService;
            _response = new ResponseDto();
        }

        // GET: api/Paises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paises>>> GetPaises()
        {
            try
            {
                var pais = await _paisesService.GetPaises();

                if(pais == null || !pais.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron paises.";
                    return NotFound(_response);
                }
                return Ok(pais);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener los paises.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        // GET: api/Paises/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Paises>> GetPaises(int id)
        {
            if(!await _paisesService.PaisesExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Pais no encontrado.";
                return NotFound(_response);
            }
            try
            {
                var pais = await _paisesService.GetPaisesById(id);
                if(pais != null)
                {
                    _response.Result = pais;
                    _response.DisplayMessage = "Pais encontrado correctamente.";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Pais no encontrado.";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al obtener el pais con ID");
                return StatusCode(500, _response);
            }
        }

        // PUT: api/Paises/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaises(int id, DtoPaises paisesDto)
        {
            if(!await _paisesService.PaisesExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Pais no encontrado.";
                return NotFound(_response);
            }
            try
            {
                var paises = await _paisesService.Update(paisesDto);
                _response.Result = paises;
                _response.DisplayMessage = "Pais actualizado correctamente.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al actualizar el pais.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return BadRequest(_response);
            }
        }

        // POST: api/Paises
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Paises>> PostPaises(DtoPaises paisesDto)
        {
            try
            {
                DtoPaises paises = await _paisesService.Create(paisesDto);
                return StatusCode(201, new {Message = "Pais creado correctamente.", Data = paises});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el pais.");
               return BadRequest(new {Message = "Error al crear el pais.", Details = ex.Message});
            }
        }

        // DELETE: api/Paises/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaises(int id)
        {
            try
            {
                bool deleted = await _paisesService.DeletePaises(id);
                if(deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new {Message = "Pais no encontrado."});
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el pais.");
                return StatusCode(500, new {Message = "Error al eliminar el pais.", Details = ex.Message});
            }
        }

        private bool PaisesExists(int id)
        {
            return _context.Paises.Any(e => e.Id == id);
        }
    }
}
