﻿// <auto-generated />
using Eaton.Mentoria.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Eaton.Mentoria.Repository.Migrations
{
    [DbContext(typeof(IMentoriaContext))]
    [Migration("20180313000503_Atualizacao_Aplicacoes")]
    partial class Atualizacao_Aplicacoes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Eaton.Mentoria.Domain.Entities.AplicacaoDomain", b =>
                {
                    b.Property<int>("AplicacaoId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Aceite");

                    b.Property<int>("MentoradoId");

                    b.Property<int>("MentoriaId");

                    b.Property<string>("justificativa")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("AplicacaoId");

                    b.HasIndex("MentoradoId");

                    b.HasIndex("MentoriaId");

                    b.ToTable("Aplicacoes");
                });

            modelBuilder.Entity("Eaton.Mentoria.Domain.Entities.CategoriaDomain", b =>
                {
                    b.Property<int>("CategoriaId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("CategoriaId");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("Eaton.Mentoria.Domain.Entities.HashesDomain", b =>
                {
                    b.Property<int>("HashesId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("UsuarioId");

                    b.HasKey("HashesId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Hashes");
                });

            modelBuilder.Entity("Eaton.Mentoria.Domain.Entities.MentoriaDomain", b =>
                {
                    b.Property<int>("MentoriaId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ativa");

                    b.Property<int>("CategoriaId");

                    b.Property<int>("SedeId");

                    b.Property<int>("UsuarioId");

                    b.HasKey("MentoriaId");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("SedeId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Mentorias");
                });

            modelBuilder.Entity("Eaton.Mentoria.Domain.Entities.NotaDomain", b =>
                {
                    b.Property<int>("NotaId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Nota");

                    b.Property<int>("UsuarioDeuNotaId")
                        .HasColumnName("UsuarioDeuNotaId");

                    b.Property<int>("UsuarioGanhouNotaId")
                        .HasColumnName("UsuarioGanhouNotaId");

                    b.HasKey("NotaId");

                    b.HasIndex("UsuarioDeuNotaId");

                    b.HasIndex("UsuarioGanhouNotaId");

                    b.ToTable("Notas");
                });

            modelBuilder.Entity("Eaton.Mentoria.Domain.Entities.PerfilDomain", b =>
                {
                    b.Property<int>("PerfilId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasMaxLength(9);

                    b.Property<string>("Foto");

                    b.Property<string>("MiniBio")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("SedeId");

                    b.Property<int>("UsuarioId");

                    b.HasKey("PerfilId");

                    b.HasIndex("SedeId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Perfis");
                });

            modelBuilder.Entity("Eaton.Mentoria.Domain.Entities.SedeDomain", b =>
                {
                    b.Property<int>("SedeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.HasKey("SedeId");

                    b.ToTable("Sedes");
                });

            modelBuilder.Entity("Eaton.Mentoria.Domain.Entities.UsuarioDomain", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Ativo");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Eaton.Mentoria.Domain.Entities.AplicacaoDomain", b =>
                {
                    b.HasOne("Eaton.Mentoria.Domain.Entities.UsuarioDomain", "Mentorado")
                        .WithMany("Aplicacoes")
                        .HasForeignKey("MentoradoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Eaton.Mentoria.Domain.Entities.MentoriaDomain", "Mentoria")
                        .WithMany("Aplicacoes")
                        .HasForeignKey("MentoriaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Eaton.Mentoria.Domain.Entities.HashesDomain", b =>
                {
                    b.HasOne("Eaton.Mentoria.Domain.Entities.UsuarioDomain", "usuario")
                        .WithMany("Hashes")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Eaton.Mentoria.Domain.Entities.MentoriaDomain", b =>
                {
                    b.HasOne("Eaton.Mentoria.Domain.Entities.CategoriaDomain", "Categoria")
                        .WithMany("Mentorias")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Eaton.Mentoria.Domain.Entities.SedeDomain", "Sede")
                        .WithMany("Mentorias")
                        .HasForeignKey("SedeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Eaton.Mentoria.Domain.Entities.UsuarioDomain", "Usuario")
                        .WithMany("Mentorias")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Eaton.Mentoria.Domain.Entities.NotaDomain", b =>
                {
                    b.HasOne("Eaton.Mentoria.Domain.Entities.UsuarioDomain", "UsuarioDeuNota")
                        .WithMany("ListaUsuarioDeuNotas")
                        .HasForeignKey("UsuarioDeuNotaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Eaton.Mentoria.Domain.Entities.UsuarioDomain", "UsuarioGanhouNota")
                        .WithMany("ListaUsuarioGanhouNotas")
                        .HasForeignKey("UsuarioGanhouNotaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Eaton.Mentoria.Domain.Entities.PerfilDomain", b =>
                {
                    b.HasOne("Eaton.Mentoria.Domain.Entities.SedeDomain", "Sede")
                        .WithMany("Perfis")
                        .HasForeignKey("SedeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Eaton.Mentoria.Domain.Entities.UsuarioDomain", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
