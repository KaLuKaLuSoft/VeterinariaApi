using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VeterinariaApi;
using VeterinariaApi.Data;
using VeterinariaApi.Interface;
using VeterinariaApi.Repositorio;
using VeterinariaApi.Seguridad;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.
builder.Services.AddControllers();

// Configuración de Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Define el documento de Swagger
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Veterinaria API",
        Description = "Una API para la gestión de la veterinaria.",
    });

    // Define el esquema de seguridad para JWT
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT en este formato: Bearer {token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Retrieve the connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

IMapper mapper = MappingConfiguration.RegisterMap().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<Token>();

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]!))
    };
});
// Fix: Specify the ServerVersion explicitly
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Inyección de dependencias de tus repositorios (lo que ya tenías)
builder.Services.AddScoped<IPaisesRepositorio, PaisesRepositorio>();
builder.Services.AddScoped<IRegionesRepositorio, RegionesRepositorio>();
builder.Services.AddScoped<ICiudadRepositorio, CiudadRepositorio>();
builder.Services.AddScoped<ISucursalesRepositorio, SucursalesRepositorio>();
builder.Services.AddScoped<IDepartamentosRepositorio, DepartamentosRepositorio>();
builder.Services.AddScoped<IRolesRepositorio, RolesRepositorio>();
builder.Services.AddScoped<IEspecialidadMedicaRepositorio, EspecialidadMedicaRepositorio>();
builder.Services.AddScoped<IModuloRepositorio, ModuloRepositorio>();
builder.Services.AddScoped<ISubModuloRepositorio, SubModuloRepositorio>();
builder.Services.AddScoped<ILoginRepositorio, LoginRepositorio>();
builder.Services.AddScoped<ILoginMenuRepositorio, LoginMenuRepositorio>();
builder.Services.AddScoped<IAccionesRepositorio, AccionesRepositorio>();
builder.Services.AddScoped<ILoginAccionesRepositorio, LoginAccionesRepositorio>();
builder.Services.AddScoped<IEmpleadoRepositorio, EmpleadoRepositorio>();
builder.Services.AddScoped<IEmpleadoEspecialidadRepositorio, EmpleadoEspecialidadRepositorio>();
builder.Services.AddScoped<ITipoTurnoRepositorio, TipoTurnoRepositorio>();
builder.Services.AddScoped<ITurnosEmpleadoRepositorio, TurnosEmpleadoRepositorio>();
builder.Services.AddScoped<ITipoAusenciaRepositorio, TipoAusenciaRepositorio>();
builder.Services.AddScoped<IAusenciaEmpleadoRepositorio, AusenciaEmpleadoRepositorio>();
builder.Services.AddScoped<IUsuarioRolRepositorio, UsuarioRolRepositorio>();
builder.Services.AddScoped<IUsuarioSucursalRepositorio, UsuarioSucursalRepositorio>();
builder.Services.AddScoped<ICriterioEvaluacionRepositorio, CriterioEvaluacionRepositorio>();
builder.Services.AddScoped<IEvaluacionEmpleadoRepositorio, EvaluacionEmpleadoRepositorio>();
builder.Services.AddScoped<ICursoCapacitacionRepositorio, CursoCapacitacionRepositorio>();
builder.Services.AddScoped<IEmpleadoCapacitacionRepositorio, EmpleadoCapacitacionRepositorio>();
builder.Services.AddScoped<ICategoriaActivoFijoRepositorio, CategoriaActivoFijoRepositorio>();
builder.Services.AddScoped<IActivosFijosRepositorio, ActivosFijosRepositorio>();
builder.Services.AddScoped<IConceptoNominasRepositorio, ConceptoNominasRepositorio>();
builder.Services.AddScoped<IMovimientoNominaRepositorio, MovimientoNominaRepositorio>();
builder.Services.AddScoped<ILogueoRepositorio, LogueoRepositorio>();
builder.Services.AddScoped<ILoginAccionesRepositorio, LoginAccionesRepositorio>();
builder.Services.AddScoped<ITipoClientesRepositorio, TipoClientesRepositorio>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Veterinaria API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseCors("PermitirTodo");

app.UseAuthorization();

app.MapControllers();

app.Run();