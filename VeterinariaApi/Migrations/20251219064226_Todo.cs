using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinariaApi.Migrations
{
    /// <inheritdoc />
    public partial class Todo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreAcciones = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acciones", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CategoriaActivoFijo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreCategoriaActivoFijo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaActivoFijo", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ConceptoNominas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreNomina = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoConcepto = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EsFijo = table.Column<ulong>(type: "bit", nullable: true, defaultValue: 0ul),
                    Descripcion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceptoNominas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CriterioEvaluacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreCriterio = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoCriterio = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriterioEvaluacion", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CursoCapacitacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreCurso = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Duracion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Proveedor = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoCapacitacion", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreDepartamento = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EspecialidadesMedicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreEspecialidad = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspecialidadesMedicas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Modulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreModulo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Codigo = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreRol = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TipoAusencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreAusencia = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RequiereAprobacion = table.Column<ulong>(type: "bit", nullable: false, defaultValue: 0ul),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoAusencia", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TipoTurno",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreTurno = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HoraInicio = table.Column<TimeSpan>(type: "time(6)", nullable: true),
                    HoraFin = table.Column<TimeSpan>(type: "time(6)", nullable: true),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoTurno", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SubModulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreSubModulo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModuloId = table.Column<int>(type: "int", nullable: false),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubModulos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubModulos_Modulos_ModuloId",
                        column: x => x.ModuloId,
                        principalTable: "Modulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Regiones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreDepartamento = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdPais = table.Column<int>(type: "int", nullable: false),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regiones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regiones_Paises_IdPais",
                        column: x => x.IdPais,
                        principalTable: "Paises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Ciudades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreCiudad = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdRegion = table.Column<int>(type: "int", nullable: false),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ciudades_Regiones_IdRegion",
                        column: x => x.IdRegion,
                        principalTable: "Regiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sucursales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreSucursal = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Direccion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdCiudad = table.Column<int>(type: "int", nullable: true),
                    Telefono = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailContacto = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sucursales_Ciudades_IdCiudad",
                        column: x => x.IdCiudad,
                        principalTable: "Ciudades",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ActivosFijos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreActivo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumeroSerie = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CategoriaActivoId = table.Column<int>(type: "int", nullable: false),
                    SucursalId = table.Column<int>(type: "int", nullable: false),
                    FechaAdquisicion = table.Column<DateTime>(type: "datetime", nullable: true),
                    CostoAdquisicion = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VidaUtil = table.Column<int>(type: "int", nullable: true),
                    UbicacionFisica = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Observaciones = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivosFijos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivosFijos_CategoriaActivoFijo_CategoriaActivoId",
                        column: x => x.CategoriaActivoId,
                        principalTable: "CategoriaActivoFijo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivosFijos_Sucursales_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "Sucursales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CodEmpleado = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Apellido = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Direccion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Celular = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ci = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaContratacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Salario = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IdDepartamento = table.Column<int>(type: "int", nullable: true),
                    IdSucursal = table.Column<int>(type: "int", nullable: true),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empleados_Departamentos_IdDepartamento",
                        column: x => x.IdDepartamento,
                        principalTable: "Departamentos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Empleados_Sucursales_IdSucursal",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursales",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AusenciaEmpleado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    TipoAusenciaId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaInicio = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaFin = table.Column<DateTime>(type: "datetime", nullable: true),
                    Motivo = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AprobadoPorEmpleado = table.Column<int>(type: "int", nullable: false),
                    FechaSolicitud = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaAprobacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AusenciaEmpleado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AusenciaEmpleado_Empleados_AprobadoPorEmpleado",
                        column: x => x.AprobadoPorEmpleado,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AusenciaEmpleado_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AusenciaEmpleado_TipoAusencia_TipoAusenciaId",
                        column: x => x.TipoAusenciaId,
                        principalTable: "TipoAusencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EmpleadoCapacitacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    CapacitacionId = table.Column<int>(type: "int", nullable: false),
                    Fecha_Capacitacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    EstadoAprobacion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Calificacion = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoCapacitacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpleadoCapacitacion_CursoCapacitacion_CapacitacionId",
                        column: x => x.CapacitacionId,
                        principalTable: "CursoCapacitacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpleadoCapacitacion_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EmpleadoEsepecialidad",
                columns: table => new
                {
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    EspecialidadId = table.Column<int>(type: "int", nullable: false),
                    FechaCertificacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoEsepecialidad", x => new { x.EmpleadoId, x.EspecialidadId });
                    table.ForeignKey(
                        name: "FK_EmpleadoEsepecialidad_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpleadoEsepecialidad_EspecialidadesMedicas_EspecialidadId",
                        column: x => x.EspecialidadId,
                        principalTable: "EspecialidadesMedicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EvaluacionEmpleado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    CriterioEvaluacionId = table.Column<int>(type: "int", nullable: false),
                    Fecha_Evaluacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Calificacion = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Comentarios = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EvaluadoPorEmpleadoId = table.Column<int>(type: "int", nullable: false),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionEmpleado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluacionEmpleado_CriterioEvaluacion_CriterioEvaluacionId",
                        column: x => x.CriterioEvaluacionId,
                        principalTable: "CriterioEvaluacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvaluacionEmpleado_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvaluacionEmpleado_Empleados_EvaluadoPorEmpleadoId",
                        column: x => x.EvaluadoPorEmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Usuario = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Contrasena = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdRol = table.Column<int>(type: "int", nullable: true),
                    Activo = table.Column<ulong>(type: "bit", nullable: true, defaultValue: 0ul),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    IdEmpleado = table.Column<int>(type: "int", nullable: true),
                    Tokens = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Expiration = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    RefreshToken = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UltimoLogin = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Login_Empleados_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Login_Roles_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MovimientosNomina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    ConceptoNominaId = table.Column<int>(type: "int", nullable: false),
                    FechaMovimiento = table.Column<DateTime>(type: "datetime", nullable: true),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PeriodoNomina = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RegistradorPorEmpleado = table.Column<int>(type: "int", nullable: false),
                    Observaciones = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientosNomina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimientosNomina_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimientosNomina_Empleados_RegistradorPorEmpleado",
                        column: x => x.RegistradorPorEmpleado,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TurnosEmpleado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    SucursalId = table.Column<int>(type: "int", nullable: false),
                    TurnoId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: true),
                    Confirmado = table.Column<ulong>(type: "bit", nullable: false, defaultValue: 0ul),
                    Observaciones = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurnosEmpleado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TurnosEmpleado_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TurnosEmpleado_Sucursales_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "Sucursales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TurnosEmpleado_TipoTurno_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "TipoTurno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LoginAcciones",
                columns: table => new
                {
                    ReglasId = table.Column<int>(type: "int", nullable: false),
                    LoginId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginAcciones", x => new { x.LoginId, x.ReglasId });
                    table.ForeignKey(
                        name: "FK_LoginAcciones_Acciones_ReglasId",
                        column: x => x.ReglasId,
                        principalTable: "Acciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoginAcciones_Login_LoginId",
                        column: x => x.LoginId,
                        principalTable: "Login",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LoginMenus",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    LoginId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginMenus", x => new { x.LoginId, x.MenuId });
                    table.ForeignKey(
                        name: "FK_LoginMenus_Login_LoginId",
                        column: x => x.LoginId,
                        principalTable: "Login",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoginMenus_SubModulos_MenuId",
                        column: x => x.MenuId,
                        principalTable: "SubModulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UsuarioRol",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRol", x => new { x.UsuarioId, x.RolId });
                    table.ForeignKey(
                        name: "FK_UsuarioRol_Login_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Login",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioRol_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UsuarioSucursal",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    SucursalId = table.Column<int>(type: "int", nullable: false),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioSucursal", x => new { x.UsuarioId, x.SucursalId });
                    table.ForeignKey(
                        name: "FK_UsuarioSucursal_Login_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Login",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioSucursal_Sucursales_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "Sucursales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ActivosFijos_CategoriaActivoId",
                table: "ActivosFijos",
                column: "CategoriaActivoId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivosFijos_NumeroSerie",
                table: "ActivosFijos",
                column: "NumeroSerie",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActivosFijos_SucursalId",
                table: "ActivosFijos",
                column: "SucursalId");

            migrationBuilder.CreateIndex(
                name: "IX_AusenciaEmpleado_AprobadoPorEmpleado",
                table: "AusenciaEmpleado",
                column: "AprobadoPorEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_AusenciaEmpleado_EmpleadoId",
                table: "AusenciaEmpleado",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_AusenciaEmpleado_TipoAusenciaId",
                table: "AusenciaEmpleado",
                column: "TipoAusenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaActivoFijo_NombreCategoriaActivoFijo",
                table: "CategoriaActivoFijo",
                column: "NombreCategoriaActivoFijo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ciudades_IdRegion",
                table: "Ciudades",
                column: "IdRegion");

            migrationBuilder.CreateIndex(
                name: "IX_ConceptoNominas_NombreNomina",
                table: "ConceptoNominas",
                column: "NombreNomina",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CriterioEvaluacion_NombreCriterio",
                table: "CriterioEvaluacion",
                column: "NombreCriterio",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CursoCapacitacion_NombreCurso",
                table: "CursoCapacitacion",
                column: "NombreCurso",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departamentos_NombreDepartamento",
                table: "Departamentos",
                column: "NombreDepartamento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoCapacitacion_CapacitacionId",
                table: "EmpleadoCapacitacion",
                column: "CapacitacionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoCapacitacion_EmpleadoId",
                table: "EmpleadoCapacitacion",
                column: "EmpleadoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoCapacitacion_Fecha_Capacitacion",
                table: "EmpleadoCapacitacion",
                column: "Fecha_Capacitacion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoEsepecialidad_EspecialidadId",
                table: "EmpleadoEsepecialidad",
                column: "EspecialidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_CodEmpleado",
                table: "Empleados",
                column: "CodEmpleado",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_IdDepartamento",
                table: "Empleados",
                column: "IdDepartamento");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_IdSucursal",
                table: "Empleados",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_EspecialidadesMedicas_NombreEspecialidad",
                table: "EspecialidadesMedicas",
                column: "NombreEspecialidad",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionEmpleado_CriterioEvaluacionId",
                table: "EvaluacionEmpleado",
                column: "CriterioEvaluacionId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionEmpleado_EmpleadoId",
                table: "EvaluacionEmpleado",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionEmpleado_EvaluadoPorEmpleadoId",
                table: "EvaluacionEmpleado",
                column: "EvaluadoPorEmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Login_IdEmpleado",
                table: "Login",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Login_IdRol",
                table: "Login",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_Login_Usuario",
                table: "Login",
                column: "Usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoginAcciones_ReglasId",
                table: "LoginAcciones",
                column: "ReglasId");

            migrationBuilder.CreateIndex(
                name: "IX_LoginMenus_MenuId",
                table: "LoginMenus",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Modulos_NombreModulo",
                table: "Modulos",
                column: "NombreModulo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosNomina_EmpleadoId",
                table: "MovimientosNomina",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosNomina_RegistradorPorEmpleado",
                table: "MovimientosNomina",
                column: "RegistradorPorEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Regiones_IdPais",
                table: "Regiones",
                column: "IdPais");

            migrationBuilder.CreateIndex(
                name: "IX_Regiones_NombreDepartamento",
                table: "Regiones",
                column: "NombreDepartamento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_NombreRol",
                table: "Roles",
                column: "NombreRol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubModulos_ModuloId",
                table: "SubModulos",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_SubModulos_NombreSubModulo",
                table: "SubModulos",
                column: "NombreSubModulo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sucursales_EmailContacto",
                table: "Sucursales",
                column: "EmailContacto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sucursales_IdCiudad",
                table: "Sucursales",
                column: "IdCiudad");

            migrationBuilder.CreateIndex(
                name: "IX_TipoAusencia_NombreAusencia",
                table: "TipoAusencia",
                column: "NombreAusencia",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TipoTurno_NombreTurno",
                table: "TipoTurno",
                column: "NombreTurno",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TurnosEmpleado_EmpleadoId",
                table: "TurnosEmpleado",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_TurnosEmpleado_SucursalId",
                table: "TurnosEmpleado",
                column: "SucursalId");

            migrationBuilder.CreateIndex(
                name: "IX_TurnosEmpleado_TurnoId",
                table: "TurnosEmpleado",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_RolId",
                table: "UsuarioRol",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioSucursal_SucursalId",
                table: "UsuarioSucursal",
                column: "SucursalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivosFijos");

            migrationBuilder.DropTable(
                name: "AusenciaEmpleado");

            migrationBuilder.DropTable(
                name: "ConceptoNominas");

            migrationBuilder.DropTable(
                name: "EmpleadoCapacitacion");

            migrationBuilder.DropTable(
                name: "EmpleadoEsepecialidad");

            migrationBuilder.DropTable(
                name: "EvaluacionEmpleado");

            migrationBuilder.DropTable(
                name: "LoginAcciones");

            migrationBuilder.DropTable(
                name: "LoginMenus");

            migrationBuilder.DropTable(
                name: "MovimientosNomina");

            migrationBuilder.DropTable(
                name: "TurnosEmpleado");

            migrationBuilder.DropTable(
                name: "UsuarioRol");

            migrationBuilder.DropTable(
                name: "UsuarioSucursal");

            migrationBuilder.DropTable(
                name: "CategoriaActivoFijo");

            migrationBuilder.DropTable(
                name: "TipoAusencia");

            migrationBuilder.DropTable(
                name: "CursoCapacitacion");

            migrationBuilder.DropTable(
                name: "EspecialidadesMedicas");

            migrationBuilder.DropTable(
                name: "CriterioEvaluacion");

            migrationBuilder.DropTable(
                name: "Acciones");

            migrationBuilder.DropTable(
                name: "SubModulos");

            migrationBuilder.DropTable(
                name: "TipoTurno");

            migrationBuilder.DropTable(
                name: "Login");

            migrationBuilder.DropTable(
                name: "Modulos");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Departamentos");

            migrationBuilder.DropTable(
                name: "Sucursales");

            migrationBuilder.DropTable(
                name: "Ciudades");

            migrationBuilder.DropTable(
                name: "Regiones");

            migrationBuilder.DropTable(
                name: "Paises");
        }
    }
}
