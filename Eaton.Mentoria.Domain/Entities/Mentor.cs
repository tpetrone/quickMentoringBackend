using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eaton.Mentoria.Domain.Entities
{
    public class Mentor
    {
       public int MentorId { get; set; }
       [ForeignKey("UsuarioId")]
       [Required]
       [stringLenght(100)]
       public Usuario usuarios { get; set; }
       public int UsuarioId { get; set; }
       
        
    }
}