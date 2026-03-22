using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using NuGet.Protocol.Plugins;
using VeterinariaApi.Data;
using VeterinariaApi.Dto;
using VeterinariaApi.Interface;
using VeterinariaApi.Models;

namespace VeterinariaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDocumentosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITipoDocumentoRepositorio _tipoDocumentoRepositorio;
        private readonly ILogger<TipoDocumentosController> _logger;
        protected ResponseDto _response;

        public TipoDocumentosController(ApplicationDbContext context, ILogger<TipoDocumentosController> logger, ITipoDocumentoRepositorio tipoDocumentoRepositorio)
        {
            _context = context;
            _logger = logger;
            _tipoDocumentoRepositorio = tipoDocumentoRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/TipoDocumentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoDocumentos>>> GetTipoDocumentos()
        {
            try
            {
                var tipodocumentos = await _tipoDocumentoRepositorio.GetTipoDocumento();
                if(tipodocumentos == null || !tipodocumentos.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontr{o Tipo de Documento";
                    return NotFound(_response);
                }
                return Ok(tipodocumentos);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener todos los Tipos de Documentos";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener todos los Tipos de Documentos", Details = ex.Message });
            }
        }

        // GET: api/TipoDocumentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoDocumentos>> GetTipoDocumentos(int id)
        {
            if(!await _tipoDocumentoRepositorio.TipoDocumentoExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Tipo de Documento no encontrado.";
                return Ok(_response);
            }
            try
            {
                var tipodocumentos = await _tipoDocumentoRepositorio.GetTipoDocumentoById(id);
                if(tipodocumentos != null)
                {
                    _response.Result = tipodocumentos;
                    _response.DisplayMessage = "Tipo de Documento encontrados correctamente. ";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Tipo de Documento no encontrado. ";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message};
                _logger.LogError(ex, "Error al obtener los Tipo de Documentos. ");
                return StatusCode(500, _response);
            }
        }

        // PUT: api/TipoDocumentos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoDocumentos(int id, DtoTipoDocumento tipoDocumentosDto)
        {
            if(!await _tipoDocumentoRepositorio.TipoDocumentoExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Tipo de Documento no encontrado. ";
                return NotFound(_response) ;
            }
            try
            {
                var tipodocumentos = await _tipoDocumentoRepositorio.Update(tipoDocumentosDto);
                _response.Result = tipodocumentos;
                _response.DisplayMessage = "Tipo de Documento actualizada correctamente. ";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.DisplayMessage = "Error al actualizar Tipo de Documento. ";
                return BadRequest(_response);
            };
        }

        // POST: api/TipoDocumentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoDocumentos>> PostTipoDocumentos(DtoTipoDocumento tipoDocumentosDto)
        {
            try
            {
                DtoTipoDocumento tipoDocumentos = await _tipoDocumentoRepositorio.Create(tipoDocumentosDto);
                return StatusCode(201, new { Message = "Tipo de Documento creada correctamente. ", Data = tipoDocumentos });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error al crear el Tipo de Documento. ");
                return BadRequest(new { Message = "Error al crear Tipo de Documento", Details = ex.Message });
            }
        }

        // DELETE: api/TipoDocumentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoDocumentos(int id)
        {
            try
            {
                bool deleted = await _tipoDocumentoRepositorio.DeleteTipoDocumento(id);
                if(deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "Tipo de Documento no encontrada. "});
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el Tipo de Documento. ");
                return StatusCode(500, new { Message = "Error al eliminar el Tipo de Documento. ", Details = ex.Message});
            }
        }

        private bool TipoDocumentosExists(int id)
        {
            return _context.TipoDocumentos.Any(e => e.Id == id);
        }
    }
}
