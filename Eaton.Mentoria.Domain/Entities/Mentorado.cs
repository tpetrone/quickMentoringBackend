using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eaton.Mentoria.Domain.Entities
{
    public class Mentorado
    {
        public int MentoradoId { get; set; }
        
        [ForeignKey("UsuarioId")]
        public Usuario Usuarios { get; set; }
        public int UsuarioId { get; set; }
        
    }
}