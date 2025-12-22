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
    public class LoginMenusController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<LoginMenusController> _logger;
        private ILoginMenuRepositorio _loginMenuRepositorio;
        protected ResponseDto _response;
        public LoginMenusController(ApplicationDbContext context, ILogger<LoginMenusController> logger, ILoginMenuRepositorio loginMenuRepositorio)
        {
            _context = context;
            _logger = logger;
            _loginMenuRepositorio = loginMenuRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/LoginMenus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoginMenu>>> GetLoginMenus()
        {
            return await _context.LoginMenus.ToListAsync();
        }

        // GET: api/LoginMenus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoginMenu>> GetLoginMenu(int id)
        {
            try
            {
                var lista = await _loginMenuRepositorio.GetLoginMenuById(id);

                if(lista == null)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "No se encontraron registros.";
                }
                else
                {
                    _response.IsSuccess = true;
                    _response.Result = lista;
                    _response.DisplayMessage = "Lista de LoginMenu obtenida exitosamente.";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return Ok(_response);
        }

        // PUT: api/LoginMenus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoginMenu(int id, LoginMenu loginMenu)
        {
            if (id != loginMenu.LoginId)
            {
                return BadRequest();
            }

            _context.Entry(loginMenu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginMenuExists(id))
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

        // POST: api/LoginMenus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LoginMenu>> PostLoginMenu(DtoLoginMenu loginMenuDto)
        {
            try
            {
                DtoLoginMenu loginMenu = await _loginMenuRepositorio.Create(loginMenuDto);
                return StatusCode(201, new {Message = "LoginMenu creado correctamente.", Data = loginMenu});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear loginmenu.");
                return BadRequest(new { Message = "Error al crear loginmenu", Details = ex.Message });
            }
        }

        // DELETE: api/LoginMenus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoginMenu(int id)
        {
            var loginMenu = await _context.LoginMenus.FindAsync(id);
            if (loginMenu == null)
            {
                return NotFound();
            }

            _context.LoginMenus.Remove(loginMenu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoginMenuExists(int id)
        {
            return _context.LoginMenus.Any(e => e.LoginId == id);
        }
    }
}
