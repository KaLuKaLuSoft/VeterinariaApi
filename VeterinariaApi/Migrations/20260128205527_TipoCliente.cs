using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinariaApi.Migrations
{
    /// <inheritdoc />
    public partial class TipoCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Login_Roles_IdRol",
                table: "Login");

            migrationBuilder.DropIndex(
                name: "IX_Login_IdRol",
                table: "Login");

            migrationBuilder.DropColumn(
                name: "IdRol",
                table: "Login");

            migrationBuilder.AddColumn<ulong>(
                name: "Activo",
                table: "UsuarioSucursal",
                type: "bit",
                nullable: true,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "Activo",
                table: "UsuarioRol",
                type: "bit",
                nullable: true,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "Activo",
                table: "TurnosEmpleado",
                type: "bit",
                nullable: true,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "Activo",
                table: "TipoTurno",
                type: "bit",
                nullable: true,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "Activo",
                table: "TipoAusencia",
                type: "bit",
                nullable: true,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "Activo",
                table: "Sucursales",
                type: "bit",
                nullable: true,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "Activo",
                table: "SubModulos",
                type: "bit",
                nullable: true,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "Activo",
                table: "Roles",
                type: "bit",
                nullable: true,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "Activo",
                table: "Regiones",
                type: "bit",
                nullable: true,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "Activo",
                table: "Paises",
                type: "bit",
                nullable: true,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "Activo",
                table: "MovimientosNomina",
                type: "bit",
                nullable: true,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "Activo",
                table: "Modulos",
                type: "bit",
                nullable: true,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "Activo",
                table: "EvaluacionEmpleado",
                type: "bit",
                nullable: true,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "EspecialidadesMedicas",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<ulong>(
                name: "Activo",
                table: "Empleados",
                type: "bit",
                nullable: true,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "Activo",
                table: "EmpleadoEsepecialidad",
                type: "bit",
                nullable: true,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "Activo",
                table: "EmpleadoCapacitacion",
                type: "bit",
                nullable: true,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "Activo",
                table: "Departamentos",
                type: "bit",
                nullable: true,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "Activo",
                table: "CursoCapacitacion",
                type: "bit",
                nullable: true,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "Activo",
                table: "CriterioEvaluacion",
                type: "bit",
                nullable: true,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "Activo",
                table: "ConceptoNominas",
                type: "bit",
                nullable: true,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "Activo",
                table: "Ciudades",
                type: "bit",
                nullable: true,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "Activo",
                table: "CategoriaActivoFijo",
                type: "bit",
                nullable: true,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "Activo",
                table: "AusenciaEmpleado",
                type: "bit",
                nullable: true,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "Activo",
                table: "ActivosFijos",
                type: "bit",
                nullable: true,
                defaultValue: 0ul);

            migrationBuilder.AddColumn<ulong>(
                name: "Activo",
                table: "Acciones",
                type: "bit",
                nullable: true,
                defaultValue: 0ul);

            migrationBuilder.CreateTable(
                name: "TipoClientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreTipo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Activo = table.Column<ulong>(type: "bit", nullable: true, defaultValue: 0ul),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoClientes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TipoClientes");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "UsuarioSucursal");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "UsuarioRol");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "TurnosEmpleado");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "TipoTurno");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "TipoAusencia");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Sucursales");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "SubModulos");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Regiones");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Paises");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "MovimientosNomina");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Modulos");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "EvaluacionEmpleado");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "EspecialidadesMedicas");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "EmpleadoEsepecialidad");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "EmpleadoCapacitacion");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Departamentos");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "CursoCapacitacion");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "CriterioEvaluacion");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "ConceptoNominas");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Ciudades");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "CategoriaActivoFijo");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "AusenciaEmpleado");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "ActivosFijos");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Acciones");

            migrationBuilder.AddColumn<int>(
                name: "IdRol",
                table: "Login",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Login_IdRol",
                table: "Login",
                column: "IdRol");

            migrationBuilder.AddForeignKey(
                name: "FK_Login_Roles_IdRol",
                table: "Login",
                column: "IdRol",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
