using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Eaton.Mentoria.Domain.Entities
{
    public class UsuarioDomain
    
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty(PropertyName = "id")]
        public int UsuarioId { get; set; }
        
        [Required]
        [StringLength(50)]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        
        [Required]
        [StringLength(20)]
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
        
        [Required]
        [StringLength(20)]
        [JsonProperty(PropertyName = "role")]
        public string Role { get; set; }

        [JsonProperty(PropertyName = "ativo")]
        public Boolean Ativo { get; set; }


        [JsonProperty(PropertyName = "perfil")]
        public virtual PerfilDomain Perfil { get; set; }

        public virtual ICollection<HashesDomain> Hashes { get; set; } 
        
        public virtual ICollection<AplicacaoDomain> Aplicacoes { get; set; } 
        public virtual ICollection<MentoriaDomain> Mentorias { get; set; }
        public virtual ICollection<NotaDomain> ListaUsuarioGanhouNotas { get; set; }
        public virtual ICollection<NotaDomain> ListaUsuarioDeuNotas { get; set; }

        }
}