using Microsoft.EntityFrameworkCore;
using VeterinariaApi.Models;

namespace VeterinariaApi.Data
{
    // Renamed the class to avoid circular dependency with the base DbContext class
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Aquí definirás las propiedades DbSet para cada una de tus tablas
        // Por ejemplo, si tienes una tabla "Productos":
        public DbSet<VeterinariaApi.Models.Paises> Paises { get; set; }
        public DbSet<VeterinariaApi.Models.Regiones> Regiones { get; set; }
        public DbSet<VeterinariaApi.Models.Ciudad> Ciudades { get; set; }
        public DbSet<VeterinariaApi.Models.Sucursales> Sucursales { get; set; }
        // Aquí podrías configurar modelos, relaciones, etc. (opcional)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Ejemplo de configuración de un modelo
            // modelBuilder.Entity<Producto>().ToTable("Productos");
        }
    // Renamed the class to avoid circular dependency with the base DbContext class

    }
}
