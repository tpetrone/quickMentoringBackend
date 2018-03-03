using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eaton.Mentoria.Domain.Entities
{
    public class Hashes
    {
        public int HashesId { get; set; }
        
        [ForeignKey("UsuarioId")]
        public Usuario usuarios { get; set; }
        public int UsuarioId { get; set; }
        public string Hash { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }
        
        
    }
}