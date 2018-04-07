using System.Linq;
using Eaton.Mentoria.Domain.Contracts;
using Eaton.Mentoria.Domain.Entities;
using Eaton.Mentoria.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Eaton.Mentoria.Repository.Repositories
{
    public class UsuarioRepository : BaseRepository<UsuarioDomain>, IUsuarioRepository
    {
        private readonly IMentoriaContext _dbContext;
        public UsuarioRepository(IMentoriaContext iusuariocontext):
        base (iusuariocontext)
        {
            _dbContext = iusuariocontext;
        }

        public UsuarioDomain BuscarPorEmail(string email)
        {
            return _dbContext.Usuarios.Include("Perfil").FirstOrDefault(x => x.Email.ToLower()==email.ToLower());
        }

        public UsuarioDomain UsuarioExiste(string email, string password)
        {
            return _dbContext.Usuarios.Include("Perfil").FirstOrDefault(x => x.Email.ToLower()==email.ToLower() && x.Password == password);
            
        }

        public bool UsuarioExiste(int id)
        {
            return _dbContext.Usuarios.AsNoTracking().Include("Perfil").Include("Perfil.Sede").Count(x => x.UsuarioId == id) >= 1;
        }

        public int Deletar(int id)
        {

            try
            {
                foreach (AplicacaoDomain i in _dbContext.Aplicacoes.Where(d => d.MentoradoId == id).ToList())
                {
                    _dbContext.Set<AplicacaoDomain>().Remove(i);
                }
                foreach (MentoriaDomain i in _dbContext.Mentorias.Where(d => d.UsuarioId == id).ToList())
                {
                    _dbContext.Set<MentoriaDomain>().Remove(i);
                }
                foreach (PerfilDomain i in _dbContext.Perfis.Where(d => d.UsuarioId== id).ToList())
                {
                    _dbContext.Set<PerfilDomain>().Remove(i);
                }

                _dbContext.SaveChanges();

                _dbContext.Set<UsuarioDomain>().Remove(_dbContext.Usuarios.First(d => d.UsuarioId == id));
                return _dbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

        }

    }
}