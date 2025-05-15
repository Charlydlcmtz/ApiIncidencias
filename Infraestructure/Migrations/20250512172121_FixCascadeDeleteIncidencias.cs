using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadeDeleteIncidencias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriasIncidencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasIncidencias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContrasenaHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposIncidencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoriaIncidenciaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposIncidencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TiposIncidencias_CategoriasIncidencias_CategoriaIncidenciaId",
                        column: x => x.CategoriaIncidenciaId,
                        principalTable: "CategoriasIncidencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incidencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoriaIncidenciaId = table.Column<int>(type: "int", nullable: false),
                    TipoIncidenciaId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    TipoIncidenciaId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incidencias_CategoriasIncidencias_CategoriaIncidenciaId",
                        column: x => x.CategoriaIncidenciaId,
                        principalTable: "CategoriasIncidencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Incidencias_TiposIncidencias_TipoIncidenciaId",
                        column: x => x.TipoIncidenciaId,
                        principalTable: "TiposIncidencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Incidencias_TiposIncidencias_TipoIncidenciaId1",
                        column: x => x.TipoIncidenciaId1,
                        principalTable: "TiposIncidencias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Incidencias_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CategoriasIncidencias",
                columns: new[] { "Id", "Descripcion", "Titulo" },
                values: new object[,]
                {
                    { 1, null, "Camiones" },
                    { 2, null, "Operadores" },
                    { 3, null, "Mantenimiento" },
                    { 4, null, "Diesel" },
                    { 5, null, "Carga/descarga" },
                    { 6, null, "Tiempos de entrega" },
                    { 7, null, "Ruta / Accidente" },
                    { 8, null, "Sistemas" }
                });

            migrationBuilder.InsertData(
                table: "TiposIncidencias",
                columns: new[] { "Id", "CategoriaIncidenciaId", "Descripcion", "Titulo" },
                values: new object[,]
                {
                    { 1, 1, null, "Fallo mecánico" },
                    { 2, 5, null, "Retraso en carga" },
                    { 3, 5, null, "Retraso en descarga" },
                    { 4, 8, null, "Problema con GPS/Tablet" },
                    { 5, 7, null, "Incidente en ruta (choque, etc.)" },
                    { 6, 4, null, "Falta de diesel" },
                    { 7, 1, null, "Daño en unidad" },
                    { 8, 3, null, "Error en documentos" },
                    { 9, 2, null, "Operador no disponible" },
                    { 10, 3, null, "Otro" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Incidencias_CategoriaIncidenciaId",
                table: "Incidencias",
                column: "CategoriaIncidenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidencias_TipoIncidenciaId",
                table: "Incidencias",
                column: "TipoIncidenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidencias_TipoIncidenciaId1",
                table: "Incidencias",
                column: "TipoIncidenciaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Incidencias_UsuarioId",
                table: "Incidencias",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TiposIncidencias_CategoriaIncidenciaId",
                table: "TiposIncidencias",
                column: "CategoriaIncidenciaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Incidencias");

            migrationBuilder.DropTable(
                name: "TiposIncidencias");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "CategoriasIncidencias");
        }
    }
}
