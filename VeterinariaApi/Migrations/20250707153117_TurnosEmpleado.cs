using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinariaApi.Migrations
{
    /// <inheritdoc />
    public partial class TurnosEmpleado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TurnosEmpleado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    SucursalId = table.Column<int>(type: "int", nullable: false),
                    TurnoId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Confirmado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Observaciones = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime(6)", nullable: true)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TurnosEmpleado");
        }
    }
}
