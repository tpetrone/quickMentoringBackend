using Eaton.Mentoria.Domain.Entities;

namespace Eaton.Mentoria.Domain.Contracts
{
    public interface ICategoriaRepository : IBaseRepository<CategoriaDomain>
    {
         bool CategoriaExiste(string nome);
    }
}