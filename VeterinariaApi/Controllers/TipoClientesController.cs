using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;
using VeterinariaApi.Models;
using VeterinariaApi.Repositorio;

namespace VeterinariaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoClientesController : ControllerBase
    {
        private readonly ITipoClientesRepositorio _tipoClienteRepositorio;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TipoClientesController> _logger;
        protected ResponseDto _response;

        public TipoClientesController(ApplicationDbContext context, ILogger<TipoClientesController> logger, ITipoClientesRepositorio tipoClienteRepositorio)
        {
            _context = context;
            _logger = logger;
            _tipoClienteRepositorio = tipoClienteRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/TipoClientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoCliente>>> GetTipoClientes()
        {
            try
            {
                var tipocliente = await _tipoClienteRepositorio.GetTipoClientes();
                if (tipocliente == null || !tipocliente.Any()) 
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron Tipo Cliente";
                    return NotFound(_response);
                }
                return Ok(tipocliente);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener los tipos clientes";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener todos los tipos clientes", Details = ex.Message });
            }
        }

        // GET: api/TipoClientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoCliente>> GetTipoCliente(int id)
        {
            if(!await _tipoClienteRepositorio.TipoClientesExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Tipo Cliente no encontrado. ";
                return NotFound(_response);
            }
            try
            {
                var tipocliente = await _tipoClienteRepositorio.GetTipoClientesById(id);
                if (tipocliente != null) 
                {
                    _response.Result = tipocliente;
                    _response.DisplayMessage = "Tipo Cliente no encontrado. ";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Tipo Cliente encontrado. ";
                    return NotFound(_response);
                }
            }
            catch (Exception ex) 
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al obtener el tipo cliente. ");
                return StatusCode(500, _response);
            }
        }

        // PUT: api/TipoClientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoCliente(int id, TipoCliente tipoCliente)
        {
            if (id != tipoCliente.Id)
            {
                return BadRequest();
            }

            _context.Entry(tipoCliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoClienteExists(id))
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

        // POST: api/TipoClientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoCliente>> PostTipoCliente(DtoTipoCliente tipoClienteDto)
        {
            try
            {
                DtoTipoCliente tipoCliente = await _tipoClienteRepositorio.Create(tipoClienteDto);
                return StatusCode(201, new { Message = "Tipo Cliente creado exitosamente", Data = tipoCliente });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el tipo cliente. ");
                return BadRequest(new { Message = "Error al crear el tipo cliente", Details = ex.Message });
            }
        }

        // DELETE: api/TipoClientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoCliente(int id)
        {
            var tipoCliente = await _context.TipoClientes.FindAsync(id);
            if (tipoCliente == null)
            {
                return NotFound();
            }

            _context.TipoClientes.Remove(tipoCliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoClienteExists(int id)
        {
            return _context.TipoClientes.Any(e => e.Id == id);
        }
    }
}
