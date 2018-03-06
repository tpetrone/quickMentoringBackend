using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eaton.Mentoria.Domain.Entities
{
    public class MentoradoDomain
    {
        public int MentoradoId { get; set; }
        
        [ForeignKey("UsuarioId")]
        public UsuarioDomain Usuarios { get; set; }
        public int UsuarioId { get; set; }
        
    }
}