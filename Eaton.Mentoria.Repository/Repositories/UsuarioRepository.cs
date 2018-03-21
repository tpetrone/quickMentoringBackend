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

        public UsuarioDomain UsuarioExiste(string email, string password, string role)
        {
            return _dbContext.Usuarios.Include("Perfil").FirstOrDefault(x => x.Email.ToLower()==email.ToLower() && x.Password== password && x.Role==role );
            
        }

        

    }
}