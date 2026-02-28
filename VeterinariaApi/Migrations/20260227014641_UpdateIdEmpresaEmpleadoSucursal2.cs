using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinariaApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIdEmpresaEmpleadoSucursal2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Paises_IdPias",
                table: "Empleados");

            migrationBuilder.DropForeignKey(
                name: "FK_Sucursales_Paises_IdPais",
                table: "Sucursales");

            migrationBuilder.RenameColumn(
                name: "IdPais",
                table: "Sucursales",
                newName: "IdEmpresa");

            migrationBuilder.RenameIndex(
                name: "IX_Sucursales_IdPais",
                table: "Sucursales",
                newName: "IX_Sucursales_IdEmpresa");

            migrationBuilder.RenameColumn(
                name: "IdPias",
                table: "Empleados",
                newName: "IdEmpresa");

            migrationBuilder.RenameIndex(
                name: "IX_Empleados_IdPias",
                table: "Empleados",
                newName: "IX_Empleados_IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Empresas_IdEmpresa",
                table: "Empleados",
                column: "IdEmpresa",
                principalTable: "Empresas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sucursales_Empresas_IdEmpresa",
                table: "Sucursales",
                column: "IdEmpresa",
                principalTable: "Empresas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Empresas_IdEmpresa",
                table: "Empleados");

            migrationBuilder.DropForeignKey(
                name: "FK_Sucursales_Empresas_IdEmpresa",
                table: "Sucursales");

            migrationBuilder.RenameColumn(
                name: "IdEmpresa",
                table: "Sucursales",
                newName: "IdPais");

            migrationBuilder.RenameIndex(
                name: "IX_Sucursales_IdEmpresa",
                table: "Sucursales",
                newName: "IX_Sucursales_IdPais");

            migrationBuilder.RenameColumn(
                name: "IdEmpresa",
                table: "Empleados",
                newName: "IdPias");

            migrationBuilder.RenameIndex(
                name: "IX_Empleados_IdEmpresa",
                table: "Empleados",
                newName: "IX_Empleados_IdPias");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Paises_IdPias",
                table: "Empleados",
                column: "IdPias",
                principalTable: "Paises",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sucursales_Paises_IdPais",
                table: "Sucursales",
                column: "IdPais",
                principalTable: "Paises",
                principalColumn: "Id");
        }
    }
}
