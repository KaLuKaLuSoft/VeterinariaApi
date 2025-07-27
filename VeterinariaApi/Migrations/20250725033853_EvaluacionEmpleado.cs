using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinariaApi.Migrations
{
    /// <inheritdoc />
    public partial class EvaluacionEmpleado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvaluacionEmpleado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    CriterioEvaluacionId = table.Column<int>(type: "int", nullable: false),
                    Fecha_Evaluacion = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Calificacion = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Comentarios = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EvaluadoPorEmpleadoId = table.Column<int>(type: "int", nullable: false),
                    Fecha_Alta = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionEmpleado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluacionEmpleado_CriterioEvaluacion_CriterioEvaluacionId",
                        column: x => x.CriterioEvaluacionId,
                        principalTable: "CriterioEvaluacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvaluacionEmpleado_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvaluacionEmpleado_Empleados_EvaluadoPorEmpleadoId",
                        column: x => x.EvaluadoPorEmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionEmpleado_CriterioEvaluacionId",
                table: "EvaluacionEmpleado",
                column: "CriterioEvaluacionId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionEmpleado_EmpleadoId",
                table: "EvaluacionEmpleado",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionEmpleado_EvaluadoPorEmpleadoId",
                table: "EvaluacionEmpleado",
                column: "EvaluadoPorEmpleadoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluacionEmpleado");
        }
    }
}
