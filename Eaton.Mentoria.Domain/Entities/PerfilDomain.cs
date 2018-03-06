using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eaton.Mentoria.Domain.Entities
{
    public class PerfilDoman
    {
        public int PerfilId{ get; set; }
        
        [ForeignKey("UsuarioId")]
        public UsuarioDomain Usuarios { get; set; }
        public int UsuarioId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string MiniBio { get; set; }
        public string Foto { get; set; }
        
        [Required]
        [StringLength(9)]
        public string Cep { get; set; }
        
        [Required]
        [StringLength(50)]
        public string sede { get; set; }





    }
}