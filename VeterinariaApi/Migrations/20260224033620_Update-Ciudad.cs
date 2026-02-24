using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinariaApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCiudad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ciudades_Regiones_IdRegion",
                table: "Ciudades");

            migrationBuilder.RenameColumn(
                name: "IdRegion",
                table: "Ciudades",
                newName: "IdPais");

            migrationBuilder.RenameIndex(
                name: "IX_Ciudades_IdRegion",
                table: "Ciudades",
                newName: "IX_Ciudades_IdPais");

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "UsuarioSucursal",
                type: "bit",
                nullable: true,
                defaultValue: 1ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 0ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "UsuarioRol",
                type: "bit",
                nullable: true,
                defaultValue: 1ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 0ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "TurnosEmpleado",
                type: "bit",
                nullable: true,
                defaultValue: 1ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 0ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "TipoTurno",
                type: "bit",
                nullable: true,
                defaultValue: 1ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 0ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "TipoClientes",
                type: "bit",
                nullable: true,
                defaultValue: 1ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 0ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "TipoAusencia",
                type: "bit",
                nullable: true,
                defaultValue: 1ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 0ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "Sucursales",
                type: "bit",
                nullable: true,
                defaultValue: 1ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 0ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "SubModulos",
                type: "bit",
                nullable: true,
                defaultValue: 1ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 0ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "Roles",
                type: "bit",
                nullable: true,
                defaultValue: 1ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 0ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "Regiones",
                type: "bit",
                nullable: true,
                defaultValue: 1ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 0ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "Paises",
                type: "bit",
                nullable: true,
                defaultValue: 1ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 0ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "MovimientosNomina",
                type: "bit",
                nullable: true,
                defaultValue: 1ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 0ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "Modulos",
                type: "bit",
                nullable: true,
                defaultValue: 1ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 0ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "Login",
                type: "bit",
                nullable: true,
                defaultValue: 1ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 0ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "EvaluacionEmpleado",
                type: "bit",
                nullable: true,
                defaultValue: 1ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 0ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "Empleados",
                type: "bit",
                nullable: true,
                defaultValue: 1ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 0ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "EmpleadoEsepecialidad",
                type: "bit",
                nullable: true,
                defaultValue: 1ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 0ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "EmpleadoCapacitacion",
                type: "bit",
                nullable: true,
                defaultValue: 1ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 0ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "Departamentos",
                type: "bit",
                nullable: true,
                defaultValue: 1ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 0ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "CursoCapacitacion",
                type: "bit",
                nullable: true,
                defaultValue: 1ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 0ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "CriterioEvaluacion",
                type: "bit",
                nullable: true,
                defaultValue: 1ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 0ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "ConceptoNominas",
                type: "bit",
                nullable: true,
                defaultValue: 1ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 0ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "Ciudades",
                type: "bit",
                nullable: true,
                defaultValue: 1ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 0ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "CategoriaActivoFijo",
                type: "bit",
                nullable: true,
                defaultValue: 1ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 0ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "AusenciaEmpleado",
                type: "bit",
                nullable: true,
                defaultValue: 1ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 0ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "ActivosFijos",
                type: "bit",
                nullable: true,
                defaultValue: 1ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 0ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "Acciones",
                type: "bit",
                nullable: true,
                defaultValue: 1ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 0ul);

            migrationBuilder.AddForeignKey(
                name: "FK_Ciudades_Paises_IdPais",
                table: "Ciudades",
                column: "IdPais",
                principalTable: "Paises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ciudades_Paises_IdPais",
                table: "Ciudades");

            migrationBuilder.RenameColumn(
                name: "IdPais",
                table: "Ciudades",
                newName: "IdRegion");

            migrationBuilder.RenameIndex(
                name: "IX_Ciudades_IdPais",
                table: "Ciudades",
                newName: "IX_Ciudades_IdRegion");

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "UsuarioSucursal",
                type: "bit",
                nullable: true,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 1ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "UsuarioRol",
                type: "bit",
                nullable: true,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 1ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "TurnosEmpleado",
                type: "bit",
                nullable: true,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 1ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "TipoTurno",
                type: "bit",
                nullable: true,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 1ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "TipoClientes",
                type: "bit",
                nullable: true,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 1ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "TipoAusencia",
                type: "bit",
                nullable: true,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 1ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "Sucursales",
                type: "bit",
                nullable: true,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 1ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "SubModulos",
                type: "bit",
                nullable: true,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 1ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "Roles",
                type: "bit",
                nullable: true,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 1ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "Regiones",
                type: "bit",
                nullable: true,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 1ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "Paises",
                type: "bit",
                nullable: true,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 1ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "MovimientosNomina",
                type: "bit",
                nullable: true,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 1ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "Modulos",
                type: "bit",
                nullable: true,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 1ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "Login",
                type: "bit",
                nullable: true,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 1ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "EvaluacionEmpleado",
                type: "bit",
                nullable: true,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 1ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "Empleados",
                type: "bit",
                nullable: true,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 1ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "EmpleadoEsepecialidad",
                type: "bit",
                nullable: true,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 1ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "EmpleadoCapacitacion",
                type: "bit",
                nullable: true,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 1ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "Departamentos",
                type: "bit",
                nullable: true,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 1ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "CursoCapacitacion",
                type: "bit",
                nullable: true,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 1ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "CriterioEvaluacion",
                type: "bit",
                nullable: true,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 1ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "ConceptoNominas",
                type: "bit",
                nullable: true,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 1ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "Ciudades",
                type: "bit",
                nullable: true,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 1ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "CategoriaActivoFijo",
                type: "bit",
                nullable: true,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 1ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "AusenciaEmpleado",
                type: "bit",
                nullable: true,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 1ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "ActivosFijos",
                type: "bit",
                nullable: true,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 1ul);

            migrationBuilder.AlterColumn<ulong>(
                name: "Activo",
                table: "Acciones",
                type: "bit",
                nullable: true,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: 1ul);

            migrationBuilder.AddForeignKey(
                name: "FK_Ciudades_Regiones_IdRegion",
                table: "Ciudades",
                column: "IdRegion",
                principalTable: "Regiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
