using System;
using Eaton.Mentoria.Domain.Entities;

namespace Eaton.Mentoria.Domain.Contracts
{
    public interface IAplicacaoRepository : IBaseRepository<AplicacaoDomain>
    {
        bool AplicacaoExiste(int mentoradoId, int mentoriaId);

    }
}