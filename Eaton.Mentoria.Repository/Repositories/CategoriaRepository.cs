using System.Linq;
using Eaton.Mentoria.Domain.Contracts;
using Eaton.Mentoria.Domain.Entities;
using Eaton.Mentoria.Repository.Context;

namespace Eaton.Mentoria.Repository.Repositories
{
    public class CategoriaRepository : BaseRepository<CategoriaDomain>, ICategoriaRepository
    {
        private readonly IMentoriaContext _dbContext;

        public CategoriaRepository(IMentoriaContext imentoriacontext):
        base(imentoriacontext)
        {
            _dbContext = imentoriacontext;
        }

        public bool CategoriaExiste(string nome)
        {
            if (_dbContext.Categorias.Any(x => x.Nome==nome))
            {
                return true;
            }

            return false;
        }
    }
}