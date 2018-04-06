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
        [JsonProperty(PropertyName = "usuario")]
        public virtual UsuarioDomain Mentorado { get; set; }

        [JsonProperty(PropertyName = "usuarioId")]
        public int MentoradoId { get; set; }
        
        [ForeignKey("MentoriaId")]
        [JsonProperty(PropertyName = "mentoria")]
        public virtual MentoriaDomain Mentoria { get; set; }

        [JsonProperty(PropertyName = "mentoriaId")]
        public int MentoriaId { get; set; }
        
        [Required]
        [StringLength(200)]
        [JsonProperty(PropertyName = "justificativa")]
        public string justificativa { get; set; }  

    }
}
