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
        public DbSet<VeterinariaApi.Models.Departamentos> Departamentos { get; set; }
        public DbSet<VeterinariaApi.Models.Roles> Roles { get; set; }
        public DbSet<VeterinariaApi.Models.EspecialidadesMedicas> EspecialidadesMedicas { get; set; }
        public DbSet<VeterinariaApi.Models.Empleados> Empleados { get; set; }
        public DbSet<VeterinariaApi.Models.Login> Login { get; set; }
        public DbSet<VeterinariaApi.Models.Modulo> Modulos { get; set; }
        public DbSet<VeterinariaApi.Models.SubModulo> SubModulos { get; set; }
        public DbSet<VeterinariaApi.Models.LoginMenu> LoginMenus { get; set; }
        public DbSet<VeterinariaApi.Models.Acciones> Acciones { get; set; }
        // Aquí podrías configurar modelos, relaciones, etc. (opcional)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginMenu>()
                .HasKey(lm => new { lm.LoginId, lm.SubMenuId });

            // Si quieres mantener las relaciones también:
            modelBuilder.Entity<LoginMenu>()
                .HasOne(lm => lm.Login)
                .WithMany()
                .HasForeignKey(lm => lm.LoginId);

            modelBuilder.Entity<LoginMenu>()
                .HasOne(lm => lm.SubModulo)
                .WithMany()
                .HasForeignKey(lm => lm.SubMenuId);

            modelBuilder.Entity<Empleados>(entity =>
            {
                entity.Property(e => e.Salario)
                      .HasColumnType("decimal(18,2)");
            });
        }

    }
}
