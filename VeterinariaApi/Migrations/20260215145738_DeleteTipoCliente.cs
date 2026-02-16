using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinariaApi.Migrations
{
    /// <inheritdoc />
    public partial class DeleteTipoCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ulong>(
                name: "IsDeleted",
                table: "TipoClientes",
                type: "bit",
                nullable: true,
                defaultValue: 0ul);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TipoClientes");
        }
    }
}
