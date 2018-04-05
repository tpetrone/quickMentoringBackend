using Eaton.Mentoria.Domain.Entities;

namespace Eaton.Mentoria.Domain.Contracts
{
    public interface IPerfilRepository : IBaseRepository<PerfilDomain>
    {
        bool PerfilExiste(string cep, byte[] foto, string minibio);
         
    }
}