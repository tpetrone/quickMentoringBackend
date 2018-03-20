using System.Linq;
using Eaton.Mentoria.Domain.Contracts;
using Eaton.Mentoria.Domain.Entities;
using Eaton.Mentoria.Repository.Context;


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

        public bool UsuarioExiste(string email, string password, string role)
        {
            if(_dbContext.Usuarios.Any(x => x.Email==email && x.Password== password && x.Role==role ))
            {
                return true;
            }
            return false;
        }

    }
}