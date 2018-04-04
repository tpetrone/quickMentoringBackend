using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Eaton.Mentoria.Domain.Entities
{
    public class HashesDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty(PropertyName = "id")]
        public int HashesId { get; set; }
        
        [ForeignKey("UsuarioId")]
        public virtual UsuarioDomain usuario { get; set; }
        public int UsuarioId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Hash { get; set; }

    }
}