using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
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
    public class EmpresasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly ILogger<EmpresasController> _logger;
        protected ResponseDto _response;
        
        public EmpresasController(ApplicationDbContext context, ILogger<EmpresasController> logger, IEmpresaRepositorio empresaRepositorio)
        {
            _context = context;
            _logger = logger;
            _empresaRepositorio = empresaRepositorio;
            _response = new ResponseDto();
        }

        // POST: api/Empresas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Empresa>> PostEmpresa([FromForm] DtoEmpresa empresaDto, IFormFile? logoFile)
        {
            try
            {
                // 1. Obtener la ruta raíz del proyecto
                string projectRoot = Directory.GetCurrentDirectory();

                // 2. Definir la ruta de wwwroot y de la carpeta logo
                string wwwrootPath = Path.Combine(projectRoot, "wwwroot");
                string folderName = "logo";
                string physicalPath = Path.Combine(wwwrootPath, folderName);

                // 3. Verificación y creación de carpetas
                // Esto creará wwwroot si no existe, y luego logo dentro de ella
                if (!Directory.Exists(physicalPath))
                {
                    Directory.CreateDirectory(physicalPath);
                    _logger.LogInformation($"Se crearon las rutas físicas: {physicalPath}");
                }

                // 4. Procesar el archivo si es que el usuario envió uno
                if (logoFile != null && logoFile.Length > 0)
                {
                    // Validar extensión
                    string extension = Path.GetExtension(logoFile.FileName).ToLower();
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };

                    if (!allowedExtensions.Contains(extension))
                    {
                        return BadRequest(new { Message = "Formato de imagen no válido." });
                    }

                    // Crear un nombre único (Guid) para evitar sobrescribir archivos
                    string fileName = $"{Guid.NewGuid()}{extension}";
                    string fullPhysicalPath = Path.Combine(physicalPath, fileName);

                    // Guardar físicamente el archivo en el servidor
                    using (var stream = new FileStream(fullPhysicalPath, FileMode.Create))
                    {
                        await logoFile.CopyToAsync(stream);
                    }

                    // 5. Asignar la ruta relativa al DTO
                    // Importante: Guardamos la ruta que el navegador usará, no la física del disco
                    empresaDto.LogoUrl = $"/{folderName}/{fileName}";
                }

                // 6. Llamar al repositorio para realizar el INSERT
                DtoEmpresa nuevaEmpresa = await _empresaRepositorio.Create(empresaDto);

                return StatusCode(201, new { Message = "Empresa creada exitosamente", Data = nuevaEmpresa });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar el registro de la empresa.");
                return BadRequest(new { Message = "Error al crear la empresa", Details = ex.Message });
            }
        }

        // GET: api/Empresas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empresa>>> GetEmpresas()
        {
            return await _context.Empresas.ToListAsync();
        }

        // GET: api/Empresas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Empresa>> GetEmpresa(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);

            if (empresa == null)
            {
                return NotFound();
            }

            return empresa;
        }

        // PUT: api/Empresas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpresa(int id, Empresa empresa)
        {
            if (id != empresa.Id)
            {
                return BadRequest();
            }

            _context.Entry(empresa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpresaExists(id))
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

        // DELETE: api/Empresas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }

            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmpresaExists(int id)
        {
            return _context.Empresas.Any(e => e.Id == id);
        }
    }
}
