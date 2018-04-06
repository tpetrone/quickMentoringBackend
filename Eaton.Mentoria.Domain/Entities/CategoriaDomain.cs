using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Eaton.Mentoria.Domain.Entities
{
    public class CategoriaDomain
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty(PropertyName = "id")]
        public int CategoriaId { get; set; }

        [Required]
        [StringLength(100)]
        [JsonProperty(PropertyName = "nome")]
        public string Nome { get; set; }
        public virtual ICollection<MentoriaDomain> Mentorias { get; set; }

        
    }
}