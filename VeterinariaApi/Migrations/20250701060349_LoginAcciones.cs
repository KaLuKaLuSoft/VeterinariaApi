using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinariaApi.Migrations
{
    /// <inheritdoc />
    public partial class LoginAcciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_LoginAcciones_ReglasId",
                table: "LoginAcciones",
                column: "ReglasId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginAcciones");
        }
    }
}
