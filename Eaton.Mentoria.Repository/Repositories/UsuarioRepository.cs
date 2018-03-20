namespace Eaton.Mentoria.Repository.Repositories
{
    public class UsuarioRepository : BaseREpository<UsuarioDomain>, IUsuarioRepository
    {
        private readonly IUsuarioContext _dbContext;
        public UsuarioRepository(IUsuarioContext iusuariocontext):
        bsase (iusuariocontext)
        {
            _dbContext = iusuariocontext;
        }

        public bool UsuarioExiste(string email, string password, string role)
        {
            if(_dbContext.Usuarios.Any(x => x.Email==email && x.Password== password && x.Role==role ))
            {
                return true;
            }
            return false;
        }


        
    }
}