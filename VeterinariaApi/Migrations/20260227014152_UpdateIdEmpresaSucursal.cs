using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinariaApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIdEmpresaSucursal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sucursales_Paises_public int? IdPais { get; set; }",
                table: "Sucursales");

            migrationBuilder.DropIndex(
                name: "IX_Sucursales_public int? IdPais { get; set; }",
                table: "Sucursales");

            migrationBuilder.DropColumn(
                name: "public int? IdPais { get; set; }",
                table: "Sucursales");

            migrationBuilder.CreateIndex(
                name: "IX_Sucursales_IdPais",
                table: "Sucursales",
                column: "IdPais");

            migrationBuilder.AddForeignKey(
                name: "FK_Sucursales_Paises_IdPais",
                table: "Sucursales",
                column: "IdPais",
                principalTable: "Paises",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sucursales_Paises_IdPais",
                table: "Sucursales");

            migrationBuilder.DropIndex(
                name: "IX_Sucursales_IdPais",
                table: "Sucursales");

            migrationBuilder.AddColumn<int>(
                name: "public int? IdPais { get; set; }",
                table: "Sucursales",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sucursales_public int? IdPais { get; set; }",
                table: "Sucursales",
                column: "public int? IdPais { get; set; }");

            migrationBuilder.AddForeignKey(
                name: "FK_Sucursales_Paises_public int? IdPais { get; set; }",
                table: "Sucursales",
                column: "public int? IdPais { get; set; }",
                principalTable: "Paises",
                principalColumn: "Id");
        }
    }
}
