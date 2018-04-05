using System.Linq;
using Eaton.Mentoria.Domain.Contracts;
using Eaton.Mentoria.Domain.Entities;
using Eaton.Mentoria.Repository.Context;

namespace Eaton.Mentoria.Repository.Repositories
{
    public class PerfilRepository : BaseRepository<PerfilDomain>, IPerfilRepository
    {
         private readonly IMentoriaContext _dbContext;
        public PerfilRepository(IMentoriaContext iusuariocontext):
        base (iusuariocontext)
        {
            _dbContext = iusuariocontext;
        }

        public bool PerfilExiste(string cep, byte[] foto, string minibio)
        {
            throw new System.NotImplementedException();
        }

        public bool UsuarioExiste(string cep, byte[] foto, string minibio)
        {
            if(_dbContext.Perfis.Any(x => x.Cep==cep && x.Foto==foto && x.MiniBio==minibio ))
            {
                return true;
            }
            return false;
        }
        
    }
}