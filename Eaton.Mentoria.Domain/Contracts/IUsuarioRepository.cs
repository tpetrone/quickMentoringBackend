using Eaton.Mentoria.Domain.Entities;

namespace Eaton.Mentoria.Domain.Contracts
{
    public interface IUsuarioRepository : IBaseRepository<UsuarioDomain>
    {
        bool UsuarioExiste(string email,string password,string role);
         
    }
}