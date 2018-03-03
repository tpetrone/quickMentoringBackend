using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eaton.Mentoria.Domain.Entities
{
    public class Aplicacao
    {
        public int AplicacaoId { get; set; }
        
        [ForeignKey("MentoradoId")]
        public Mentorado Mentorados { get; set; }
        public int MentoradoId { get; set; }
        
        [ForeignKey("MentoriaId")]
        public Mentoria Mentorias { get; set; }
        public int MentoriaId { get; set; }
        public string justificativa { get; set; }  

        public ICollection<Mentoria> Mentoria { get; set; } 
        

        
    }
}
