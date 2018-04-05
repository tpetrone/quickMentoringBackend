using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Eaton.Mentoria.Repository.Migrations
{
    public partial class alteracaotabelaperfil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Foto",
                table: "Perfis",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Foto",
                table: "Perfis",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);
        }
    }
}
