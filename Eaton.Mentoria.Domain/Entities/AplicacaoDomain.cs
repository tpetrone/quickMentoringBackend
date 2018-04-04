using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Eaton.Mentoria.Domain.Entities
{
    public class AplicacaoDomain
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty(PropertyName = "id")]
        public int AplicacaoId { get; set; }

        //Aceite é a resposta à requisição de mentoria do mentorado para o mentor. Inicialmente o valor é nulo (bool?)
        public bool? Aceite { get; set; }
        
        [ForeignKey("MentoradoId")]
        public virtual UsuarioDomain Mentorado { get; set; }
        public int MentoradoId { get; set; }
        
        [ForeignKey("MentoriaId")]
        public virtual MentoriaDomain Mentoria { get; set; }
        public int MentoriaId { get; set; }
        
        [Required]
        [StringLength(200)]
        public string justificativa { get; set; }  

    }
}
