using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class ClientesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IClientesRepositorio _clientesRepositorio;
        private readonly ILogger<ClientesController> _logger;
        protected ResponseDto _response;

        public ClientesController(ApplicationDbContext context, ILogger<ClientesController> logger, IClientesRepositorio clientesRepositorio)
        {
            _context = context;
            _logger = logger;
            _clientesRepositorio = clientesRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clientes>>> GetClientes()
        {
            try
            {
                // 1. Extraer el IdEmpresa del Claim que generamos en el Token
                var idEmpresaClaim = User.FindFirst("IdEmpresa")?.Value;

                if (string.IsNullOrEmpty(idEmpresaClaim))
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontró la empresa en el token de seguridad.";
                    return Unauthorized(_response);
                }

                int idEmpresa = int.Parse(idEmpresaClaim);

                // 2. Pasar el idEmpresa al repositorio
                var clientes = await _clientesRepositorio.GetClientes(idEmpresa);

                if (clientes == null || !clientes.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron clientes para esta empresa.";
                    return NotFound(_response);
                }

                _response.IsSuccess = true;
                _response.Result = clientes;
                _response.DisplayMessage = "Lista de clientes obtenida con éxito.";

                return Ok(clientes); // O return Ok(_response) según como manejes tu objeto DtoResponse
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener los clientes.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, _response);
            }
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Clientes>> GetClientes(int id, int idEmpresa)
        {
            if(!await _clientesRepositorio.ClientesExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Cliente no encontrado.";
                return Ok(_response);
            }
            try
            {
                var clientes = await _clientesRepositorio.GetClientesById(id, idEmpresa);
                if (clientes != null)
                {
                    _response.Result = clientes;
                    _response.DisplayMessage = "Cliente encontrado correctamente.";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Cliente no encontrado.";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                _logger.LogError(ex, "Error al obtener el cliente");
                return StatusCode(500, _response);
            }
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientes(int id, DtoClientes clientesDto)
        {
            if(!await _clientesRepositorio.ClientesExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Cliente no encontrado.";
                return NotFound(_response);
            }
            try
            {
                var clientes = await _clientesRepositorio.Update(clientesDto);
                _response.Result = clientes;
                _response.DisplayMessage = "Cliente actualizado correctamente.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.DisplayMessage = "Error al actualizar el cliente.";
                return BadRequest(_response);
            }
        }

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Clientes>> PostClientes(DtoClientes clientesDto)
        {
            try
            {
                DtoClientes clientes = await _clientesRepositorio.Create(clientesDto);
                return StatusCode(201, new { Message = "Cliente creado correctamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el cliente");
                return BadRequest(new { Message = "Error al crear el cliente", Details = ex.Message });
            }
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientes(int id)
        {
            try
            {
                // Extraemos el ID de la empresa desde el Token por seguridad
                var idEmpresaClaim = User.FindFirst("IdEmpresa")?.Value;
                if (string.IsNullOrEmpty(idEmpresaClaim)) return Unauthorized();

                int idEmpresa = int.Parse(idEmpresaClaim);

                bool deleted = await _clientesRepositorio.DeleteClientes(id, idEmpresa);

                if (deleted)
                {
                    return NoContent(); // 204 Exitoso
                }
                else
                {
                    return NotFound(new { Message = "Cliente no encontrado o no pertenece a su empresa" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al eliminar el cliente", Details = ex.Message });
            }
        }

        private bool ClientesExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
