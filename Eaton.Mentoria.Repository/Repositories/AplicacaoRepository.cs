using System.Linq;
using Eaton.Mentoria.Domain.Contracts;
using Eaton.Mentoria.Domain.Entities;
using Eaton.Mentoria.Repository.Context;

namespace Eaton.Mentoria.Repository.Repositories
{
    public class AplicacaoRepository : BaseRepository<AplicacaoDomain>, IAplicacaoRepository
    {
         private readonly IMentoriaContext _dbContext;
        public AplicacaoRepository(IMentoriaContext imentoriacontext):
        base(imentoriacontext)
        {
            _dbContext = imentoriacontext;
        }

         public bool AplicacaoExiste(int mentoradoId, int mentoriaId)
        {
            if (_dbContext.Aplicacoes.Any(x => x.MentoradoId==mentoradoId && x.MentoriaId == mentoriaId))
            {
                return true;
            }
            return false;
        }
    }
}