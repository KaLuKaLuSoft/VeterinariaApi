using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
using System.Net;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace VeterinariaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConceptoNominasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ConceptoNominasController> _logger;
        protected ResponseDto _response;
        private readonly IConceptoNominasRepositorio _conceptoNominasRepositorio;

        public ConceptoNominasController(ApplicationDbContext context, ILogger<ConceptoNominasController> logger, IConceptoNominasRepositorio conceptoNominasRepositorio)
        {
            _context = context;
            _logger = logger;
            _conceptoNominasRepositorio = conceptoNominasRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/ConceptoNominas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConceptoNominas>>> GetConceptoNominas()
        {
            try
            {
                var conceptoNominas = await _conceptoNominasRepositorio.GetConceptosNominas();
                if (conceptoNominas == null || !conceptoNominas.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron conceptos de nómina.";
                    return NotFound(_response);
                }
                return Ok(conceptoNominas);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener los conceptos de nómina.";
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(500, new { Message = "Error al obtener todos los Conceptos de Nómina", Details = ex.Message });
            }
        }

        // GET: api/ConceptoNominas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConceptoNominas>> GetConceptoNominas(int id)
        {
            if(!await _conceptoNominasRepositorio.ConceptoNominaExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Concepto de nómina no encontrado.";
                return Ok(_response);
            }
            try
            {
                var conceptoNominas = await _conceptoNominasRepositorio.GetConceptosNominasById(id);
                if (conceptoNominas != null)
                {
                    _response.Result = conceptoNominas;
                    _response.DisplayMessage = "Concepto de nómina no encontrado.";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Concepto de nómina no encontrado.";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al obtener el Concepto de Nómina");
                return StatusCode(500, _response);
            }
        }

        // PUT: api/ConceptoNominas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConceptoNominas(int id, DtoConceptoNominas conceptoNominasDto)
        {
            if(!await _conceptoNominasRepositorio.ConceptoNominaExists(id))
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Concepto de nómina no encontrado.";
                return NotFound(_response);
            }
            try
            {
                var conceptoNominas = await _conceptoNominasRepositorio.Update(conceptoNominasDto);
                _response.Result = conceptoNominas;
                _response.DisplayMessage= "Concepto de nómina actualizado correctamente.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _logger.LogError(ex, "Error al actualizar el Concepto de Nómina");
                return BadRequest(_response);
            }
        }

        // POST: api/ConceptoNominas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ConceptoNominas>> PostConceptoNominas(DtoConceptoNominas conceptoNominasDto)
        {
            try
            {
                DtoConceptoNominas createdConceptoNomina = await _conceptoNominasRepositorio.Create(conceptoNominasDto);
                return StatusCode(201, new { Message = "Concepto de nómina creado correctamente.", Data = createdConceptoNomina });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el Concepto de Nómina");
                return BadRequest(new { Message = "Error al crear el Concepto de Nómina", Details = ex.Message });
            }
        }

        // DELETE: api/ConceptoNominas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConceptoNominas(int id)
        {
            try
            {
                bool deleted = await _conceptoNominasRepositorio.DeleteConceptoNomina(id);
                if (deleted)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound(new { Message = "Concepto de nómina no encontrado." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el Concepto de Nómina");
                return StatusCode(500, new { Message = "Error al eliminar el Concepto de Nómina", Details = ex.Message });
            }
        }

        private bool ConceptoNominasExists(int id)
        {
            return _context.ConceptoNominas.Any(e => e.Id == id);
        }
    }
}
