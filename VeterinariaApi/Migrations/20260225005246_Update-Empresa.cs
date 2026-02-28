using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinariaApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmpresa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresas_Ciudades_IdCiudad",
                table: "Empresas");

            migrationBuilder.RenameColumn(
                name: "IdCiudad",
                table: "Empresas",
                newName: "IdPais");

            migrationBuilder.RenameIndex(
                name: "IX_Empresas_IdCiudad",
                table: "Empresas",
                newName: "IX_Empresas_IdPais");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresas_Paises_IdPais",
                table: "Empresas",
                column: "IdPais",
                principalTable: "Paises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresas_Paises_IdPais",
                table: "Empresas");

            migrationBuilder.RenameColumn(
                name: "IdPais",
                table: "Empresas",
                newName: "IdCiudad");

            migrationBuilder.RenameIndex(
                name: "IX_Empresas_IdPais",
                table: "Empresas",
                newName: "IX_Empresas_IdCiudad");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresas_Ciudades_IdCiudad",
                table: "Empresas",
                column: "IdCiudad",
                principalTable: "Ciudades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
