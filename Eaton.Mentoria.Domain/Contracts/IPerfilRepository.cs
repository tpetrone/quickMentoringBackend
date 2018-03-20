using Eaton.Mentoria.Domain.Entities;

namespace Eaton.Mentoria.Domain.Contracts
{
    public interface IPerfilRepository : IBaseRepository<PerfilDomain>
    {
        bool PerfilExiste(string cep, string foto,string minibio);
         
    }
}