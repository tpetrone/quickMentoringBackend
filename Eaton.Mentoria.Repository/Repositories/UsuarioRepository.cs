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

        

    }
}