using System.Linq;
using Eaton.Mentoria.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Eaton.Mentoria.Repository.Context
{
    public class IMentoriaContext : DbContext
    {
        public IMentoriaContext(DbContextOptions<IMentoriaContext> options)
        :base(options)
        {
            
        }
        public DbSet<AplicacaoDomain> Aplicacoes { get; set; }
        public DbSet<CategoriaDomain> Categorias { get; set; }
        public DbSet<PerfilDomain> Perfis { get; set; }
        public DbSet<HashesDomain> Hashes { get; set; }  
        public DbSet<MentoriaDomain> Mentorias { get; set; }             
        public DbSet<UsuarioDomain> Usuarios { get; set; }
        public DbSet<NotaDomain> Notas { get; set; }
        public DbSet<SedeDomain> Sedes { get; set; }         
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            


            modelBuilder.Entity<AplicacaoDomain>().ToTable("Aplicacoes");
            modelBuilder.Entity<CategoriaDomain>().ToTable("Categorias");
            modelBuilder.Entity<HashesDomain>().ToTable("Hashes");            
            modelBuilder.Entity<PerfilDomain>().ToTable("Perfis");
            modelBuilder.Entity<MentoriaDomain>().ToTable("Mentorias");
            modelBuilder.Entity<UsuarioDomain>().ToTable("Usuarios");
            modelBuilder.Entity<NotaDomain>().ToTable("Notas");
            modelBuilder.Entity<SedeDomain>().ToTable("Sedes");

            //Fluente API para resolver a questão dos dois tipos de notas (dada e recebida) definidas na mesma classe

            modelBuilder.Entity<NotaDomain>().Property(x => x.UsuarioDeuNotaId).HasColumnName("UsuarioDeuNotaId");
            modelBuilder.Entity<NotaDomain>().Property(x => x.UsuarioGanhouNotaId).HasColumnName("UsuarioGanhouNotaId");

            modelBuilder.Entity<UsuarioDomain>()
                    .HasMany(c => c.ListaUsuarioDeuNotas)
                    .WithOne(e => e.UsuarioDeuNota)
                    .HasForeignKey(e => e.UsuarioDeuNotaId);

            modelBuilder.Entity<UsuarioDomain>()
                    .HasMany(c => c.ListaUsuarioGanhouNotas)
                    .WithOne(e => e.UsuarioGanhouNota)
                    .HasForeignKey(e => e.UsuarioGanhouNotaId);


            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);

        }
    }
   
}
