using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Eaton.Mentoria.Repository.Migrations
{
    public partial class BancoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Sedes",
                columns: table => new
                {
                    SedeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sedes", x => x.SedeId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ativo = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 20, nullable: false),
                    Role = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Hashes",
                columns: table => new
                {
                    HashesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Hash = table.Column<string>(maxLength: 100, nullable: false),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hashes", x => x.HashesId);
                    table.ForeignKey(
                        name: "FK_Hashes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mentorias",
                columns: table => new
                {
                    MentoriaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ativa = table.Column<bool>(nullable: false),
                    CategoriaId = table.Column<int>(nullable: false),
                    SedeId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mentorias", x => x.MentoriaId);
                    table.ForeignKey(
                        name: "FK_Mentorias_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mentorias_Sedes_SedeId",
                        column: x => x.SedeId,
                        principalTable: "Sedes",
                        principalColumn: "SedeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mentorias_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notas",
                columns: table => new
                {
                    NotaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nota = table.Column<decimal>(nullable: false),
                    UsuarioDeuNotaId = table.Column<int>(nullable: false),
                    UsuarioGanhouNotaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notas", x => x.NotaId);
                    table.ForeignKey(
                        name: "FK_Notas_Usuarios_UsuarioDeuNotaId",
                        column: x => x.UsuarioDeuNotaId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notas_Usuarios_UsuarioGanhouNotaId",
                        column: x => x.UsuarioGanhouNotaId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Perfis",
                columns: table => new
                {
                    PerfilId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cep = table.Column<string>(maxLength: 9, nullable: false),
                    Foto = table.Column<string>(nullable: true),
                    MiniBio = table.Column<string>(maxLength: 50, nullable: false),
                    SedeId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfis", x => x.PerfilId);
                    table.ForeignKey(
                        name: "FK_Perfis_Sedes_SedeId",
                        column: x => x.SedeId,
                        principalTable: "Sedes",
                        principalColumn: "SedeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Perfis_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Aplicacoes",
                columns: table => new
                {
                    AplicacaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MentoradoId = table.Column<int>(nullable: false),
                    MentoriaId = table.Column<int>(nullable: false),
                    justificativa = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aplicacoes", x => x.AplicacaoId);
                    table.ForeignKey(
                        name: "FK_Aplicacoes_Usuarios_MentoradoId",
                        column: x => x.MentoradoId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Aplicacoes_Mentorias_MentoriaId",
                        column: x => x.MentoriaId,
                        principalTable: "Mentorias",
                        principalColumn: "MentoriaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aplicacoes_MentoradoId",
                table: "Aplicacoes",
                column: "MentoradoId");

            migrationBuilder.CreateIndex(
                name: "IX_Aplicacoes_MentoriaId",
                table: "Aplicacoes",
                column: "MentoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Hashes_UsuarioId",
                table: "Hashes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Mentorias_CategoriaId",
                table: "Mentorias",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mentorias_SedeId",
                table: "Mentorias",
                column: "SedeId");

            migrationBuilder.CreateIndex(
                name: "IX_Mentorias_UsuarioId",
                table: "Mentorias",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_UsuarioDeuNotaId",
                table: "Notas",
                column: "UsuarioDeuNotaId");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_UsuarioGanhouNotaId",
                table: "Notas",
                column: "UsuarioGanhouNotaId");

            migrationBuilder.CreateIndex(
                name: "IX_Perfis_SedeId",
                table: "Perfis",
                column: "SedeId");

            migrationBuilder.CreateIndex(
                name: "IX_Perfis_UsuarioId",
                table: "Perfis",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aplicacoes");

            migrationBuilder.DropTable(
                name: "Hashes");

            migrationBuilder.DropTable(
                name: "Notas");

            migrationBuilder.DropTable(
                name: "Perfis");

            migrationBuilder.DropTable(
                name: "Mentorias");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Sedes");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
