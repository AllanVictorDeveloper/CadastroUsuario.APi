using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CadastroUsuario.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CriandoTabelaFotoEPessoa : Migration
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
                    CPF = table.Column<string>(type: "VARCHAR(11)", maxLength: 11, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "DATE", nullable: false),
                    Sexo = table.Column<string>(type: "text", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Imagem = table.Column<byte[]>(type: "BYTEA", nullable: false),
                    NomeHash = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    Extensao = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Principal = table.Column<bool>(type: "boolean", nullable: false),
                    PessoaId = table.Column<int>(type: "integer", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fotos_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fotos_PessoaId",
                table: "Fotos",
                column: "PessoaId");

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
                name: "Fotos");

            migrationBuilder.DropTable(
                name: "Pessoas");
        }
    }
}
