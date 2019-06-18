using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tasks.Data.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataDeAlteracao = table.Column<DateTime>(nullable: true),
                    DataDeCadastro = table.Column<DateTime>(nullable: false),
                    DataDeExclusao = table.Column<DateTime>(nullable: true),
                    Excluido = table.Column<bool>(nullable: false),
                    Conclusao = table.Column<DateTime>(nullable: true),
                    Descricao = table.Column<string>(maxLength: 500, nullable: true),
                    Titulo = table.Column<string>(maxLength: 150, nullable: true),
                    UsuarioId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataDeAlteracao = table.Column<DateTime>(nullable: true),
                    DataDeCadastro = table.Column<DateTime>(nullable: false),
                    DataDeExclusao = table.Column<DateTime>(nullable: true),
                    Excluido = table.Column<bool>(nullable: false),
                    Apelido = table.Column<string>(maxLength: 80, nullable: true),
                    Email = table.Column<string>(maxLength: 120, nullable: true),
                    Senha = table.Column<string>(maxLength: 35, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
