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
        public virtual UsuarioDomain Usuario { get; set; }
        public int UsuarioId { get; set; }
        
        [ForeignKey("CategoriaId")]
        public virtual CategoriaDomain Categoria { get; set; }
        public int CategoriaId { get; set; }
        public Boolean Ativa { get; set; }
        public Boolean Online { get; set; }
        public string Nome { get; set; }
        
        [ForeignKey("SedeId")]
        public virtual SedeDomain Sede { get; set; }

        public int SedeId { get; set; }

        public virtual ICollection<AplicacaoDomain> Aplicacoes { get; set; }
    

    }
}