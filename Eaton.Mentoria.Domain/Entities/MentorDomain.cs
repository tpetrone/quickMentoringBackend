using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eaton.Mentoria.Domain.Entities
{
    public class MentorDomain
    {
       public int MentorId { get; set; }
       [ForeignKey("UsuarioId")]
       public UsuarioDomain usuarios { get; set; }
       public int UsuarioId { get; set; }
       
        
    }
}