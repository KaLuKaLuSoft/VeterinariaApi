using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinariaApi.Migrations
{
    /// <inheritdoc />
    public partial class EmpleadoEspecialidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoEsepecialidad_EspecialidadId",
                table: "EmpleadoEsepecialidad",
                column: "EspecialidadId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmpleadoEsepecialidad");
        }
    }
}
