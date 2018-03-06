using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eaton.Mentoria.Domain.Entities
{
    public class AplicacaoDomain
    {
        public int AplicacaoId { get; set; }
        
        [ForeignKey("MentoradoId")]
        public MentoradoDomain Mentorados { get; set; }
        public int MentoradoId { get; set; }
        
        [ForeignKey("MentoriaId")]
        public MentoriaDomain Mentorias { get; set; }
        public int MentoriaId { get; set; }
        
        [Required]
        [StringLength(200)]
        public string justificativa { get; set; }  

        public ICollection<MentoriaDomain> Mentoria { get; set; } 
        

        
    }
}
