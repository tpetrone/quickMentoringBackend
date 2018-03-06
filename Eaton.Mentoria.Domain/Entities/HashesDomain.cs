using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eaton.Mentoria.Domain.Entities
{
    public class HashesDomain
    {
        public int HashesId { get; set; }
        
        [ForeignKey("UsuarioId")]
        public UsuarioDomain usuarios { get; set; }
        public int UsuarioId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Hash { get; set; }

        public ICollection<UsuarioDomain> Usuarios { get; set; }
        
        
    }
}