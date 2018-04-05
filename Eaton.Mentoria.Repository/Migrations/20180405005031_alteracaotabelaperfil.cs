using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Eaton.Mentoria.Repository.Migrations
{
    public partial class alteracaotabelaperfil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("Foto", "Perfis");

            migrationBuilder.AddColumn<byte[]>(
                name: "Foto",
                table: "Perfis",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("Foto", "Perfis");
            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "Perfis",
                nullable: true);
        }
    }
}
