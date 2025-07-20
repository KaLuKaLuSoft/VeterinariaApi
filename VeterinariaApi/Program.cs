using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VeterinariaApi;
using VeterinariaApi.Data;
using VeterinariaApi.Interface;
using VeterinariaApi.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Retrieve the connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
IMapper mapper = MappingConfiguration.RegisterMap().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Fix: Specify the ServerVersion explicitly
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
