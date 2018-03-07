using System.Collections.Generic;

namespace Eaton.Mentoria.Domain.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> Listar(string[] includes = null);

        int Atualizar(T dados);

        int Inserir(T dados);

        int Deletar(T dados);

        T BuscarPorId(int id, string[] includes = null);

         
    }
}