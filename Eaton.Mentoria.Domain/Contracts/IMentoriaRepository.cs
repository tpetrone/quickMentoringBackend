using Eaton.Mentoria.Domain.Entities;

namespace Eaton.Mentoria.Domain.Contracts
{
    public interface IMentoriaRepository : IBaseRepository<MentoriaDomain>
    {
        bool MentoriaExiste(int usuarioId, int categoriaId, int sedeId);

        int Deletar(int id);
    }
}