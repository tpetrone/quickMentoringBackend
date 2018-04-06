using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Eaton.Mentoria.Domain.Entities
{
    public class MentoriaDomain
    
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty(PropertyName = "id")]
        public int MentoriaId { get; set; }
        
        [ForeignKey("UsuarioId")]
        [JsonProperty(PropertyName = "usuario")]
        public virtual UsuarioDomain Usuario { get; set; }

        [JsonProperty(PropertyName = "usuarioId")]
        public int UsuarioId { get; set; }
        
        [ForeignKey("CategoriaId")]
        [JsonProperty(PropertyName = "categoria")]
        public virtual CategoriaDomain Categoria { get; set; }

        [JsonProperty(PropertyName = "categoriaId")]
        public int CategoriaId { get; set; }

        [JsonProperty(PropertyName = "ativa")]
        public Boolean Ativa { get; set; }

        [JsonProperty(PropertyName = "online")]
        public Boolean Online { get; set; }

        [JsonProperty(PropertyName = "nome")]
        public string Nome { get; set; }
        
        [ForeignKey("SedeId")]
        [JsonProperty(PropertyName = "sede")]
        public virtual SedeDomain Sede { get; set; }

        [JsonProperty(PropertyName = "sedeId")]
        public int SedeId { get; set; }

        public virtual ICollection<AplicacaoDomain> Aplicacoes { get; set; }
    

    }
}