using Eaton.Mentoria.Domain.Entities;

namespace Eaton.Mentoria.Domain.Contracts
{
    public interface ISedeRepository : IBaseRepository<SedeDomain>
    {
         bool SedeExiste(string nome);
         
    }
}