using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Eaton.Mentoria.Repository.Migrations
{
    public partial class Atualizacao_Aplicacoes_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Aceite",
                table: "Aplicacoes",
                nullable: true,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Aceite",
                table: "Aplicacoes",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
