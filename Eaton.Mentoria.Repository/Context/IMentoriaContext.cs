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
        public DbSet<HashesDomain> Hashes { get; set; }
        //public DbSet<MentoradoDomain> Mentorados { get; set; }
        //public DbSet<MentorDomain> Mentores { get; set; }
        public DbSet<PerfilDoman> Perfil { get; set; }
        public DbSet<UsuarioDomain> Usuarios { get; set; }
        public DbSet<NotaDomain> Notas { get; set; }
         
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<AplicacaoDomain>().ToTable("Aplicacoes");
            modelBuilder.Entity<CategoriaDomain>().ToTable("Categorias");
            modelBuilder.Entity<HashesDomain>().ToTable("Hashes");
            //modelBuilder.Entity<MentoradoDomain>().ToTable("Mentores");
            //modelBuilder.Entity<MentoradoDomain>().ToTable("Mentorado");
            modelBuilder.Entity<PerfilDoman>().ToTable("Perfil");
            modelBuilder.Entity<UsuarioDomain>().ToTable("Usuarios");
            modelBuilder.Entity<NotaDomain>().ToTable("Notas");

            base.OnModelCreating(modelBuilder);

        }
    }
}
