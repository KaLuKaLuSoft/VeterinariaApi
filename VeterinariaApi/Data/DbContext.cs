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
        public DbSet<VeterinariaApi.Models.LoginAcciones> LoginAcciones { get; set; }
        public DbSet<VeterinariaApi.Models.EmpleadoEsepecialidad> EmpleadoEsepecialidad { get; set; }
        public DbSet<VeterinariaApi.Models.TipoTurno> TipoTurno { get; set; }
        public DbSet<VeterinariaApi.Models.TurnosEmpleado> TurnosEmpleado { get; set; }
        public DbSet<VeterinariaApi.Models.TipoAusencia> TipoAusencia { get; set; }
        public DbSet<VeterinariaApi.Models.AusenciaEmpleado> AusenciaEmpleado { get; set; }
        public DbSet<VeterinariaApi.Models.UsuarioRol> UsuarioRol { get; set; }
        public DbSet<VeterinariaApi.Models.UsuarioSucursal> UsuarioSucursal { get; set; }
        // Aquí podrías configurar modelos, relaciones, etc. (opcional)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Menu
            // Menu
            modelBuilder.Entity<LoginMenu>()
            .HasKey(lm => new { lm.LoginId, lm.SubMenuId });

            modelBuilder.Entity<LoginMenu>()
                .HasOne(lm => lm.Login)
                .WithMany()
                .HasForeignKey(lm => lm.LoginId);

            modelBuilder.Entity<LoginMenu>()
                .HasOne(lm => lm.SubModulo)
                .WithMany()
                .HasForeignKey(lm => lm.SubMenuId);
            #endregion
            #region EmpleadoEspecialidad
            modelBuilder.Entity<EmpleadoEsepecialidad>()
        .HasKey(ee => new { ee.EmpleadoId, ee.EspecialidadId });

            modelBuilder.Entity<EmpleadoEsepecialidad>()
                .HasOne(ee => ee.Empleado)
                .WithMany()
                .HasForeignKey(ee => ee.EmpleadoId);

            modelBuilder.Entity<EmpleadoEsepecialidad>()
                .HasOne(ee => ee.Especialidad)
                .WithMany()
                .HasForeignKey(ee => ee.EspecialidadId); 
            #endregion
            #region Reglas
            //Reglas
            modelBuilder.Entity<LoginAcciones>()
                .HasKey(lm => new { lm.LoginId, lm.ReglasId });

            modelBuilder.Entity<LoginAcciones>()
                .HasOne(lm => lm.Login)
                .WithMany()
                .HasForeignKey(lm => lm.LoginId);

            modelBuilder.Entity<LoginAcciones>()
                .HasOne(lm => lm.Regla)
                .WithMany()
                .HasForeignKey(lm => lm.ReglasId);
            #endregion
            #region UsuarioRol
            modelBuilder.Entity<UsuarioRol>()
                .HasKey(lm => new { lm.UsuarioId, lm.RolId });
    
            modelBuilder.Entity<UsuarioRol>()
                .HasOne(lm => lm.Usuario)
                .WithMany()
                .HasForeignKey(lm => lm.UsuarioId);

            modelBuilder.Entity<UsuarioRol>()
                .HasOne(lm => lm.Roles)
                .WithMany()
                .HasForeignKey(lm => lm.RolId);
            #endregion
            #region UsuarioSucursal
            modelBuilder.Entity<UsuarioSucursal>()
                .HasKey(us => new { us.UsuarioId, us.SucursalId });

            modelBuilder.Entity<UsuarioSucursal>()
                .HasOne(us => us.Usuario)
                .WithMany()
                .HasForeignKey(us => us.UsuarioId);

            modelBuilder.Entity<UsuarioSucursal>()
                .HasOne(us => us.Sucursal)
                .WithMany()
                .HasForeignKey(us => us.SucursalId); 
            #endregion
            //Decimales
            modelBuilder.Entity<Empleados>(entity =>
            {
                entity.Property(e => e.Salario)
                      .HasColumnType("decimal(18,2)");
            });
        }

    }
}
