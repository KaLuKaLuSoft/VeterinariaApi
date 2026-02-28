using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinariaApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIdEmpresaEmpleadoSucursal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdPais",
                table: "Sucursales",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "public int? IdPais { get; set; }",
                table: "Sucursales",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdPias",
                table: "Empleados",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sucursales_public int? IdPais { get; set; }",
                table: "Sucursales",
                column: "public int? IdPais { get; set; }");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_IdPias",
                table: "Empleados",
                column: "IdPias");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Paises_IdPias",
                table: "Empleados",
                column: "IdPias",
                principalTable: "Paises",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sucursales_Paises_public int? IdPais { get; set; }",
                table: "Sucursales",
                column: "public int? IdPais { get; set; }",
                principalTable: "Paises",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Paises_IdPias",
                table: "Empleados");

            migrationBuilder.DropForeignKey(
                name: "FK_Sucursales_Paises_public int? IdPais { get; set; }",
                table: "Sucursales");

            migrationBuilder.DropIndex(
                name: "IX_Sucursales_public int? IdPais { get; set; }",
                table: "Sucursales");

            migrationBuilder.DropIndex(
                name: "IX_Empleados_IdPias",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "IdPais",
                table: "Sucursales");

            migrationBuilder.DropColumn(
                name: "public int? IdPais { get; set; }",
                table: "Sucursales");

            migrationBuilder.DropColumn(
                name: "IdPias",
                table: "Empleados");
        }
    }
}
