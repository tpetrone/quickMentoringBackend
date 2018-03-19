using System.Linq;
using Eaton.Mentoria.Domain.Contracts;
using Eaton.Mentoria.Domain.Entities;
using Eaton.Mentoria.Repository.Context;

namespace Eaton.Mentoria.Repository.Repositories
{
    public class SedeRepository : BaseRepository<SedeDomain>, ISedeRepository
    {
         private readonly IMentoriaContext _dbContext;

        public SedeRepository(IMentoriaContext imentoriacontext):
        base(imentoriacontext)
        {
            _dbContext = imentoriacontext;
        }

        public bool SedeExiste(string nome)
        {
            if (_dbContext.Sedes.Any(x => x.Nome==nome))
            {
                return true;
            }

            return false;
        }
        
        
    }

}