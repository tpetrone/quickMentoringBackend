using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Eaton.Mentoria.Repository.Migrations
{
    public partial class Atualizacao_Aplicacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Aceite",
                table: "Aplicacoes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aceite",
                table: "Aplicacoes");
        }
    }
}
