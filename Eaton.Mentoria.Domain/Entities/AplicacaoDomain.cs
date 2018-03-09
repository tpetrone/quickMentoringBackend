using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eaton.Mentoria.Domain.Entities
{
    public class AplicacaoDomain
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AplicacaoId { get; set; }
        
        [ForeignKey("MentoradoId")]
        public UsuarioDomain Usuario { get; set; }
        public int UsuarioId { get; set; }
        
        [ForeignKey("MentoriaId")]
        public MentoriaDomain Mentoria { get; set; }
        public int MentoriaId { get; set; }
        
        [Required]
        [StringLength(200)]
        public string justificativa { get; set; }  

        

    
        

        
    }
}
