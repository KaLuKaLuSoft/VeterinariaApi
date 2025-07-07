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
    public class LoginAccionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<LoginAccionesController> _logger;
        private ILoginAccionesRepositorio _loginaccionRepositorio;
        protected ResponseDto _response;
        public LoginAccionesController(ApplicationDbContext context, ILogger<LoginAccionesController> logger, ILoginAccionesRepositorio loginAccionesRepositorio)
        {
            _context = context;
            _logger = logger;
            _loginaccionRepositorio = loginAccionesRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/LoginAcciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoginAcciones>>> GetLoginAcciones()
        {
            return await _context.LoginAcciones.ToListAsync();
        }

        // GET: api/LoginAcciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoginAcciones>> GetLoginAcciones(int id)
        {
            var loginAcciones = await _context.LoginAcciones.FindAsync(id);

            if (loginAcciones == null)
            {
                return NotFound();
            }

            return loginAcciones;
        }

        // PUT: api/LoginAcciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoginAcciones(int id, LoginAcciones loginAcciones)
        {
            if (id != loginAcciones.LoginId)
            {
                return BadRequest();
            }

            _context.Entry(loginAcciones).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginAccionesExists(id))
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

        // POST: api/LoginAcciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LoginAcciones>> PostLoginAcciones(DtoLoginAcciones loginAccionesDto)
        {
            try
            {
                DtoLoginAcciones loginAcciones = await _loginaccionRepositorio.Create(loginAccionesDto);
                return StatusCode(201, new { Message = "LoginAccion creado correctamente.", Data = loginAcciones });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear loginaccion.");
                return BadRequest(new { Message = "Error al crear login.", Details = ex.Message});
            }
        }
            
        // DELETE: api/LoginAcciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoginAcciones(int id)
        {
            var loginAcciones = await _context.LoginAcciones.FindAsync(id);
            if (loginAcciones == null)
            {
                return NotFound();
            }

            _context.LoginAcciones.Remove(loginAcciones);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoginAccionesExists(int id)
        {
            return _context.LoginAcciones.Any(e => e.LoginId == id);
        }
    }
}
