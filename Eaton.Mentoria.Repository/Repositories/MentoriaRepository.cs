using System.Linq;
using Eaton.Mentoria.Domain.Contracts;
using Eaton.Mentoria.Domain.Entities;
using Eaton.Mentoria.Repository.Context;

namespace Eaton.Mentoria.Repository.Repositories
{
    public class MentoriaRepository : BaseRepository<MentoriaDomain>, IMentoriaRepository
    {
        private readonly IMentoriaContext _dbContext;

        public MentoriaRepository(IMentoriaContext imentoriacontext):
        base (imentoriacontext)
        {
            _dbContext = imentoriacontext;
        }

        public bool MentoriaExiste(int usuarioId, int categoriaId, int sedeId)
        {
            if (_dbContext.Mentorias.Any(x => x.UsuarioId==usuarioId && x.CategoriaId == categoriaId && x.SedeId == sedeId))
            {
                return true;
            }
            return false;
        }

        public int Deletar(int id)
        {

            try
            {
                foreach (AplicacaoDomain i in _dbContext.Aplicacoes.Where(d=> d.MentoriaId == id).ToList())
                {
                    _dbContext.Set<AplicacaoDomain>().Remove(i);
                }
                _dbContext.SaveChanges();

                _dbContext.Set<MentoriaDomain>().Remove(_dbContext.Mentorias.First(d=> d.MentoriaId == id));
                return _dbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

        }
    }
}