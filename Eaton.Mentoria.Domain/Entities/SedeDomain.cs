using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Eaton.Mentoria.Domain.Entities
{
    public class SedeDomain
    {   
       [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       [JsonProperty(PropertyName = "id")]
       public int SedeId {get; set;}


        [JsonProperty(PropertyName = "nome")]
        public string Nome {get; set;}

       public virtual ICollection<PerfilDomain> Perfis { get; set; } 

        public virtual ICollection<MentoriaDomain> Mentorias { get; set; } 
    }
}