using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CadastroUsuario.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CriandoTabelaPessoas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    SobreNome = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    CPF = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "DATE", nullable: false),
                    Sexo = table.Column<string>(type: "text", nullable: true),
                    Foto = table.Column<string>(type: "text", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CPF_Unique",
                table: "Pessoas",
                column: "CPF",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pessoas");
        }
    }
}
