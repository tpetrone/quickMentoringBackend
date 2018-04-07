using Eaton.Mentoria.Domain.Entities;

namespace Eaton.Mentoria.Domain.Contracts
{
    public interface IUsuarioRepository : IBaseRepository<UsuarioDomain>
    {
        UsuarioDomain UsuarioExiste(string email,string password);
        UsuarioDomain BuscarPorEmail(string email);

        bool UsuarioExiste(int id);

        int Deletar(int id);
    }
}