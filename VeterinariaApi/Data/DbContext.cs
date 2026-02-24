using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using VeterinariaApi.Models;

namespace VeterinariaApi.Data
{
    // Renamed the class to avoid circular dependency with the base DbContext class
    public class ApplicationDbContext : DbContext
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
        public DbSet<VeterinariaApi.Models.CriteriosEvaluacion> CriterioEvaluacion { get; set; }
        public DbSet<VeterinariaApi.Models.EvaluacionEmpleado> EvaluacionEmpleado { get; set; }
        public DbSet<VeterinariaApi.Models.CursoCapacitacion> CursoCapacitacion { get; set; }
        public DbSet<VeterinariaApi.Models.EmpleadoCapacitacion> EmpleadoCapacitacion { get; set; } 
        public DbSet<VeterinariaApi.Models.CategoriaActivoFijo> CategoriaActivoFijo { get; set; }
        public DbSet<VeterinariaApi.Models.ActivosFijos> ActivosFijos { get; set; }
        public DbSet<VeterinariaApi.Models.ConceptoNominas> ConceptoNominas { get; set; }
        public DbSet<VeterinariaApi.Models.MovimientosNomina> MovimientosNomina { get; set; }
        public DbSet<VeterinariaApi.Models.TipoCliente> TipoClientes { get; set; }
        public DbSet<VeterinariaApi.Models.Empresa> Empresas { get; set; }
        // Aquí podrías configurar modelos, relaciones, etc. (opcional)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Menu
            // Menu
            modelBuilder.Entity<LoginMenu>()
            .HasKey(lm => new { lm.LoginId, lm.MenuId });

            modelBuilder.Entity<LoginMenu>()
                .HasOne(lm => lm.Login)
                .WithMany()
                .HasForeignKey(lm => lm.LoginId);

            modelBuilder.Entity<LoginMenu>()
                .HasOne(lm => lm.SubModulo)
                .WithMany()
                .HasForeignKey(lm => lm.MenuId);
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
            #region Decimales
            modelBuilder.Entity<Empleados>(entity =>
                {
                    entity.Property(e => e.Salario)
                          .HasColumnType("decimal(18,2)");
                });

            modelBuilder.Entity<EvaluacionEmpleado>(entity =>
            {
                entity.Property(e => e.Calificacion)
                      .HasColumnType("decimal(18,2)");
            });
            modelBuilder.Entity<EmpleadoCapacitacion>(entity =>
            {
                entity.Property(e => e.Calificacion)
                      .HasColumnType("decimal(18,2)");
            });
            modelBuilder.Entity<ActivosFijos>(entity =>
            {
                entity.Property(e => e.CostoAdquisicion)
                      .HasColumnType("decimal(18,2)");
            });
            modelBuilder.Entity<MovimientosNomina>(entity =>
            {
                entity.Property(e => e.Monto)
                      .HasColumnType("decimal(18,2)");
            });
            #endregion
            #region Unique y MexLenth

            modelBuilder.Entity<Acciones>()
                .Property(l => l.NombreAcciones)
                .HasMaxLength(100);
            modelBuilder.Entity<Acciones>()
                .Property(l => l.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(true);
            modelBuilder.Entity<Acciones>()
                .Property(l => l.Descripcion)
                .HasMaxLength(255);
            modelBuilder.Entity<Acciones>()
                .Property(l =>l.Fecha_Alta)
                .HasColumnType("datetime");
            modelBuilder.Entity<Acciones>()
                .Property(l => l.Fecha_Modificacion)
                .HasColumnType("datetime");

            modelBuilder.Entity<AusenciaEmpleado>()
                .Property(r => r.Estado)
                .HasMaxLength(50);
            modelBuilder.Entity<AusenciaEmpleado>()
                .Property(r => r.Motivo)
                .HasMaxLength(255);
            modelBuilder.Entity<AusenciaEmpleado>()
                .Property(r => r.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(true);
            modelBuilder.Entity<AusenciaEmpleado>()
                .Property(r => r.FechaInicio)
                .HasColumnType("datetime");
            modelBuilder.Entity<AusenciaEmpleado>()
                .Property(r => r.FechaFin)
                .HasColumnType("datetime");
            modelBuilder.Entity<AusenciaEmpleado>()
                .Property(r => r.FechaSolicitud)
                .HasColumnType("datetime");
            modelBuilder.Entity<AusenciaEmpleado>()
                .Property(r => r.FechaAprobacion)
                .HasColumnType("datetime");
            modelBuilder.Entity<AusenciaEmpleado>()
                .Property(r => r.Fecha_Alta)
                .HasColumnType("datetime");
            modelBuilder.Entity<AusenciaEmpleado>()
                .Property(r => r.Fecha_Modificacion)
                .HasColumnType("datetime");

            modelBuilder.Entity<Ciudad>()
                .Property(c => c.NombreCiudad)
                .HasMaxLength(100);
            modelBuilder.Entity<Ciudad>()
                .Property(c => c.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(true);
            modelBuilder.Entity<Ciudad>()
                .Property(c => c.Fecha_Alta)
                .HasColumnType("datetime");
            modelBuilder.Entity<Ciudad>()
                .Property(c => c.Fecha_Modificacion)
                .HasColumnType("datetime");

            modelBuilder.Entity<CriteriosEvaluacion>()
                .Property(c => c.NombreCriterio)
                .HasMaxLength(100);
            modelBuilder.Entity<CriteriosEvaluacion>()
                .Property(c => c.Descripcion)
                .HasMaxLength(255);
            modelBuilder.Entity<CriteriosEvaluacion>()
                .Property(c => c.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(true);
            modelBuilder.Entity<CriteriosEvaluacion>()
                .Property(c => c.TipoCriterio)
                .HasMaxLength(50);
            modelBuilder.Entity<CriteriosEvaluacion>()
                .HasIndex(c => c.NombreCriterio)
                .IsUnique();
            modelBuilder.Entity<CriteriosEvaluacion>()
                .Property(c => c.Fecha_Alta)
                .HasColumnType("datetime");
            modelBuilder.Entity<CriteriosEvaluacion>()
                .Property(c => c.Fecha_Modificacion)
                .HasColumnType("datetime");

            modelBuilder.Entity<CursoCapacitacion>()
                .Property(c => c.NombreCurso)
                .HasMaxLength(100);
            modelBuilder.Entity<CursoCapacitacion>()
                .Property(c => c.Descripcion)
                .HasMaxLength(100);
            modelBuilder.Entity<CursoCapacitacion>()
                .Property(c => c.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(true);
            modelBuilder.Entity<CursoCapacitacion>()
                .HasIndex(c => c.NombreCurso)
                .IsUnique();
            modelBuilder.Entity<CursoCapacitacion>()
                .Property(e => e.Proveedor)
                .HasMaxLength(100);
            modelBuilder.Entity<CursoCapacitacion>()
                .Property(e => e.Duracion)
                .HasMaxLength(50);
            modelBuilder.Entity<CursoCapacitacion>()
                .Property(c => c.Fecha_Alta)
                .HasColumnType("datetime");
            modelBuilder.Entity<CursoCapacitacion>()
                .Property(c => c.Fecha_Modificacion)
                .HasColumnType("datetime");

            modelBuilder.Entity<Departamentos>()
                .Property(d => d.NombreDepartamento)
                .HasMaxLength(100);
            modelBuilder.Entity<Departamentos>()
                .Property(d => d.Descripcion)
                .HasMaxLength(255);
            modelBuilder.Entity<Departamentos>()
                .Property(d => d.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(true);
            modelBuilder.Entity<Departamentos>()
                .HasIndex(d => d.NombreDepartamento)
                .IsUnique();
            modelBuilder.Entity<Departamentos>()
                .Property(d => d.Fecha_Alta)
                .HasColumnType("datetime");
            modelBuilder.Entity<Departamentos>()
                .Property(d => d.Fecha_Modificacion)
                .HasColumnType("datetime");

            modelBuilder.Entity<Empleados>()
                .Property(e => e.CodEmpleado)
                .HasMaxLength(50);
            modelBuilder.Entity<Empleados>()
                .HasIndex(c => c.CodEmpleado)
                .IsUnique();
            modelBuilder.Entity<Empleados>()
                .Property(e => e.Empleado)
                .HasMaxLength(100);
            modelBuilder.Entity<Empleados>()
                .Property(e => e.Celular)
                .HasMaxLength(15);
            modelBuilder.Entity<Empleados>()
                .Property(e => e.Ci)
                .HasMaxLength(20);
            modelBuilder.Entity<Empleados>()
                .Property(e => e.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(true);
            modelBuilder.Entity<Empleados>()
                .Property(e => e.Fecha_Alta)
                .HasColumnType("datetime");
            modelBuilder.Entity<Empleados>()
                .Property(e => e.Fecha_Modificacion)
                .HasColumnType("datetime");

            modelBuilder.Entity<EspecialidadesMedicas>()
                .Property(e => e.NombreEspecialidad)
                .HasMaxLength(100);
            modelBuilder.Entity<EspecialidadesMedicas>()
                .Property(e => e.Descripcion)
                .HasMaxLength(255);
            modelBuilder.Entity<EspecialidadesMedicas>()
                .HasIndex(e => e.NombreEspecialidad)
                .IsUnique();
            modelBuilder.Entity<EspecialidadesMedicas>()
                .Property(e => e.Fecha_Alta)
                .HasColumnType("datetime");
            modelBuilder.Entity<EspecialidadesMedicas>()
                .Property(e => e.Fecha_Modificacion)
                .HasColumnType("datetime");

            modelBuilder.Entity<Login>()
                .Property(l => l.Usuario)
                .HasMaxLength(50);
            modelBuilder.Entity<Login>()
                .HasIndex(l => l.Usuario)
                .IsUnique();
            modelBuilder.Entity<Login>()
                .Property(l => l.Contrasena)
                .HasMaxLength(255);
            modelBuilder.Entity<Login>()
                .Property(l => l.Tokens)
                .HasMaxLength(1000);
            modelBuilder.Entity<Login>()
                .Property(l => l.RefreshToken)
                .HasMaxLength(1000);
            modelBuilder.Entity<Login>()
                .Property(l => l.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(true);
            modelBuilder.Entity<Login>()
                .Property(l => l.Fecha_Alta)
                .HasColumnType("datetime");
            modelBuilder.Entity<Login>()
                .Property(l => l.Fecha_Modificacion)
                .HasColumnType("datetime");

            modelBuilder.Entity<Modulo>()
                .Property(m => m.NombreModulo)
                .HasMaxLength(100);
            modelBuilder.Entity<Modulo>()
                .HasIndex(m => m.NombreModulo)
                .IsUnique();
            modelBuilder.Entity<Modulo>()
                .Property(m => m.Descripcion)
                .HasMaxLength(255);
            modelBuilder.Entity<Modulo>()
                .Property(m => m.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(true);
            modelBuilder.Entity<Modulo>()
                .Property(m => m.Fecha_Alta)
                .HasColumnType("datetime");
            modelBuilder.Entity<Modulo>()
                .Property(m => m.Fecha_Modificacion)
                .HasColumnType("datetime");

            modelBuilder.Entity<Paises>()
                .Property(p => p.Nombre)
                .HasMaxLength(100);
            modelBuilder.Entity<Modulo>()
                .HasIndex(m => m.NombreModulo)
                .IsUnique();
            modelBuilder.Entity<Paises>()
                .Property(p => p.Codigo)
                .HasMaxLength(10);
            modelBuilder.Entity<Paises>()
                .Property(p => p.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(true);
            modelBuilder.Entity<Paises>()
                .Property(p => p.Fecha_Alta)
                .HasColumnType("datetime");
            modelBuilder.Entity<Paises>()
                .Property(p => p.Fecha_Modificacion)
                .HasColumnType("datetime");

            modelBuilder.Entity<Regiones>()
                .Property(r => r.NombreDepartamento)
                .HasMaxLength(100);
            modelBuilder.Entity<Regiones>()
                .HasIndex(r => r.NombreDepartamento)
                .IsUnique();
            modelBuilder.Entity<Regiones>()
                .Property(r => r.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(true);
            modelBuilder.Entity<Regiones>()
                .Property(r => r.Fecha_Alta)
                .HasColumnType("datetime");
            modelBuilder.Entity<Regiones>()
                .Property(r => r.Fecha_Modificacion)
                .HasColumnType("datetime");

            modelBuilder.Entity<Roles>()
                .Property(r => r.NombreRol)
                .HasMaxLength(100);
            modelBuilder.Entity<Roles>()
                .HasIndex(r => r.NombreRol)
                .IsUnique();
            modelBuilder.Entity<Roles>()
                .Property(r => r.Descripcion)
                .HasMaxLength(255);
            modelBuilder.Entity<Roles>()
                .Property(r => r.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(true);
            modelBuilder.Entity<Roles>()
                .Property(r => r.Fecha_Alta)
                .HasColumnType("datetime");
            modelBuilder.Entity<Roles>()
                .Property(r => r.Fecha_Modificacion)
                .HasColumnType("datetime");

            modelBuilder.Entity<SubModulo>()
                .Property(sm => sm.NombreSubModulo)
                .HasMaxLength(100);
            modelBuilder.Entity<SubModulo>()
                .HasIndex(sm => sm.NombreSubModulo)
                .IsUnique();
            modelBuilder.Entity<SubModulo>()
                .Property(sm => sm.Descripcion)
                .HasMaxLength(255);
            modelBuilder.Entity<SubModulo>()
                .Property(sm => sm.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(true);
            modelBuilder.Entity<SubModulo>()
                .Property(sm => sm.Fecha_Alta)
                .HasColumnType("datetime");
            modelBuilder.Entity<SubModulo>()
                .Property(sm => sm.Fecha_Modificacion)
                .HasColumnType("datetime");

            modelBuilder.Entity<Sucursales>()
                .Property(s => s.NombreSucursal)
                .HasMaxLength(100);
            modelBuilder.Entity<Sucursales>()
                .Property(s => s.Direccion)
                .HasMaxLength(200);
            modelBuilder.Entity<Sucursales>()
                .Property(s => s.Telefono)
                .HasMaxLength(15);
            modelBuilder.Entity<Sucursales>()
                .HasIndex(s => s.EmailContacto)
                .IsUnique();
            modelBuilder.Entity<Sucursales>()
                .Property(s => s.EmailContacto)
                .HasMaxLength(100);
            modelBuilder.Entity<Sucursales>()
                .Property(s => s.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(true);
            modelBuilder.Entity<Sucursales>()
                .Property(s => s.Fecha_Alta)
                .HasColumnType("datetime");
            modelBuilder.Entity<Sucursales>()
                .Property(s => s.Fecha_Modificacion)
                .HasColumnType("datetime");

            modelBuilder.Entity<TipoAusencia>()
                .Property(t => t.NombreAusencia)
                .HasMaxLength(100);
            modelBuilder.Entity<TipoAusencia>()
                .HasIndex(t => t.NombreAusencia)
                .IsUnique();
            modelBuilder.Entity<TipoAusencia>()
                .Property(t => t.RequiereAprobacion)
                .HasColumnType("bit")
                .HasDefaultValue(false);
            modelBuilder.Entity<TipoAusencia>()
                .Property(t => t.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(true);
            modelBuilder.Entity<TipoAusencia>()
                .Property(t => t.Fecha_Alta)
                .HasColumnType("datetime");
            modelBuilder.Entity<TipoAusencia>()
                .Property(t => t.Fecha_Modificacion)
                .HasColumnType("datetime");

            modelBuilder.Entity<TipoTurno>()
                .Property(t => t.NombreTurno)
                .HasMaxLength(100);
            modelBuilder.Entity<TipoTurno>()
                .HasIndex(t => t.NombreTurno)
                .IsUnique();
            modelBuilder.Entity<TipoTurno>()
                .Property(t => t.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(true);
            modelBuilder.Entity<TipoTurno>()
                .Property(t => t.Fecha_Alta)
                .HasColumnType("datetime");
            modelBuilder.Entity<TipoTurno>()
                .Property(t => t.Fecha_Modificacion)
                .HasColumnType("datetime");

            modelBuilder.Entity<TurnosEmpleado>()
                .Property(te => te.Fecha)
                .HasColumnType("datetime");
            modelBuilder.Entity<TurnosEmpleado>()
                .Property(t => t.Observaciones)
                .HasMaxLength(255);
            modelBuilder.Entity<TurnosEmpleado>()
                .Property(t => t.Confirmado)
                .HasColumnType("bit")
                .HasDefaultValue(false);
            modelBuilder.Entity<TurnosEmpleado>()
                .Property(t => t.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(true);
            modelBuilder.Entity<TurnosEmpleado>()
                .Property(t => t.Fecha_Alta)
                .HasColumnType("datetime");
            modelBuilder.Entity<TurnosEmpleado>()
                .Property(t => t.Fecha_Modificacion)
                .HasColumnType("datetime");

            modelBuilder.Entity<EmpleadoCapacitacion>()
                .Property(ec => ec.Fecha_Capacitacion)
                .HasColumnType("datetime");
            modelBuilder.Entity<EmpleadoCapacitacion>()
                .Property(ec => ec.EstadoAprobacion)
                .HasMaxLength(50);
            modelBuilder.Entity<EmpleadoCapacitacion>()
                .HasIndex(ec => ec.EmpleadoId)
                .IsUnique();
            modelBuilder.Entity<EmpleadoCapacitacion>()
                .HasIndex(ec => ec.Fecha_Capacitacion)
                .IsUnique();
            modelBuilder.Entity<EmpleadoCapacitacion>()
                .HasIndex(ec => ec.CapacitacionId)
                .IsUnique();
            modelBuilder.Entity<EmpleadoCapacitacion>()
                .Property(ec => ec.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(true);
            modelBuilder.Entity<EmpleadoCapacitacion>()
                .Property(ec => ec.Fecha_Alta)
                .HasColumnType("datetime");
            modelBuilder.Entity<EmpleadoCapacitacion>()
                .Property(ec => ec.Fecha_Modificacion)
                .HasColumnType("datetime");

            modelBuilder.Entity<CategoriaActivoFijo>()
                .Property(c => c.NombreCategoriaActivoFijo)
                .HasMaxLength(100);
            modelBuilder.Entity<CategoriaActivoFijo>()
                .Property(c => c.Descripcion)
                .HasMaxLength(255);
            modelBuilder.Entity<CategoriaActivoFijo>()
                .HasIndex(c => c.NombreCategoriaActivoFijo)
                .IsUnique();
            modelBuilder.Entity<CategoriaActivoFijo>()
                .Property(c => c.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(true);
            modelBuilder.Entity<CategoriaActivoFijo>()
                .Property(c => c.Fecha_Alta)
                .HasColumnType("datetime");
            modelBuilder.Entity<CategoriaActivoFijo>()
                .Property(c => c.Fecha_Modificacion)
                .HasColumnType("datetime");

            modelBuilder.Entity<ActivosFijos>()
                .Property(a => a.NombreActivo)
                .HasMaxLength(100);
            modelBuilder.Entity<ActivosFijos>()
                .Property(a => a.NumeroSerie)
                .HasMaxLength(100);
            modelBuilder.Entity<ActivosFijos>()
                .Property(a => a.UbicacionFisica)
                .HasMaxLength(255);
            modelBuilder.Entity<ActivosFijos>()
                .Property(a => a.Estado)
                .HasMaxLength(100); // Activo, Inactivo, En Mantenimiento, Retirado
            modelBuilder.Entity<ActivosFijos>()
                .Property(a => a.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(true);
            modelBuilder.Entity<ActivosFijos>()
                .Property(a => a.Observaciones)
                .HasMaxLength(255);
            modelBuilder.Entity<ActivosFijos>()
                .HasIndex(a => a.NumeroSerie)
                .IsUnique();
            modelBuilder.Entity<ActivosFijos>()
                .Property(a => a.FechaAdquisicion)
                .HasColumnType("datetime");
            modelBuilder.Entity<ActivosFijos>()
                .Property(a => a.Fecha_Alta)
                .HasColumnType("datetime");
            modelBuilder.Entity<ActivosFijos>()
                .Property(a => a.Fecha_Modificacion)
                .HasColumnType("datetime");

            modelBuilder.Entity<ConceptoNominas>()
                .Property(cn => cn.NombreNomina)
                .HasMaxLength(100);
            modelBuilder.Entity<ConceptoNominas>()
                .Property(cn => cn.TipoConcepto)
                .HasMaxLength(100);
            modelBuilder.Entity<ConceptoNominas>()
                .Property(cn => cn.EsFijo)
                .HasColumnType("bit")
                .HasDefaultValue(false);
            modelBuilder.Entity<ConceptoNominas>()
                .Property(cn => cn.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(true);
            modelBuilder.Entity<ConceptoNominas>()
                .Property(cn => cn.Descripcion)
                .HasMaxLength(255);
            modelBuilder.Entity<ConceptoNominas>()
                .HasIndex(cn => cn.NombreNomina)
                .IsUnique();
            modelBuilder.Entity<ConceptoNominas>()
                .Property(cn => cn.Fecha_Alta)
                .HasColumnType("datetime");
            modelBuilder.Entity<ConceptoNominas>()
                .Property(cn => cn.Fecha_Modificacion)
                .HasColumnType("datetime");

            modelBuilder.Entity<MovimientosNomina>()
                .Property(mn => mn.PeriodoNomina)
                .HasMaxLength(150);
            modelBuilder.Entity<MovimientosNomina>()
                .Property(mn => mn.Observaciones)
                .HasMaxLength(255);
            modelBuilder.Entity<MovimientosNomina>()
                .Property(mn => mn.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(true);
            modelBuilder.Entity<MovimientosNomina>()
                .Property(mn => mn.FechaMovimiento)
                .HasColumnType("datetime");
            modelBuilder.Entity<MovimientosNomina>()
                .Property(mn => mn.Fecha_Alta)
                .HasColumnType("datetime");
            modelBuilder.Entity<MovimientosNomina>()
                .Property(mn => mn.Fecha_Modificacion)
                .HasColumnType("datetime");

            modelBuilder.Entity<TipoCliente>()
                .Property(tc => tc.NombreTipo)
                .HasMaxLength(100);
            modelBuilder.Entity<TipoCliente>()
                .Property(tc => tc.Descripcion)
                .HasMaxLength(255);
            modelBuilder.Entity<TipoCliente>()
                .Property(tc => tc.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(true);
            modelBuilder.Entity<TipoCliente>()
                .Property(tc => tc.IsDeleted)
                .HasColumnType("bit")
                .HasDefaultValue(false);
            modelBuilder.Entity<TipoCliente>()
                .Property(tc => tc.Fecha_Alta)
                .HasColumnType("datetime");
            modelBuilder.Entity<TipoCliente>()
                .Property(tc => tc.Fecha_Modificacion)
                .HasColumnType("datetime");

            modelBuilder.Entity<EmpleadoEsepecialidad>()
                .Property(ee => ee.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(true);

            modelBuilder.Entity<EvaluacionEmpleado>()
                .Property(e => e.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(true);

            modelBuilder.Entity<UsuarioRol>()
                .Property(lm => lm.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(true);

            modelBuilder.Entity<UsuarioSucursal>()
                .Property(us => us.Activo)
                .HasColumnType("bit")
                .HasDefaultValue(true);

            modelBuilder.Entity<Empresa>()
                .Property(e => e.NombreComercial)
                .HasMaxLength(150);
            modelBuilder.Entity<Empresa>()
                .Property(e => e.RazonSocial)
                .HasMaxLength(150);
            modelBuilder.Entity<Empresa>()
                .Property(e => e.LogoUrl)
                .HasMaxLength(500);
            modelBuilder.Entity<Empresa>()
                .Property(e => e.PlanSuscripcion)
                .HasConversion<string>();
            modelBuilder.Entity<Empresa>()
                .Property(e => e.EstadoCuenta)
                .HasConversion<string>();
            modelBuilder.Entity<Empresa>()
                .Property(e => e.FechaRegistro)
                .HasColumnType("datetime");
            #endregion
        }
    }
}
