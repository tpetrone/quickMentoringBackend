using System.Linq;
using Eaton.Mentoria.Domain.Contracts;
using Eaton.Mentoria.Domain.Entities;
using Eaton.Mentoria.Repository.Context;

namespace Eaton.Mentoria.Repository.Repositories
{
    public class MentoriaRepository : BaseRepository<MentoriaDomain>, IMentoriaRepository
    {
        private readonly IMentoriaContext _dbContext;

        public MentoriaRepository(IMentoriaContext imentoriacontext):
        base (imentoriacontext)
        {
            _dbContext = imentoriacontext;
        }

        public bool MentoriaExiste(int usuarioId, int categoriaId, int sedeId)
        {
            if (_dbContext.Mentorias.Any(x => x.UsuarioId==usuarioId && x.CategoriaId == categoriaId && x.SedeId == sedeId))
            {
                return true;
            }
            return false;
        }
    }
}