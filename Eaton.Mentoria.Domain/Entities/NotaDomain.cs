using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Eaton.Mentoria.Domain.Entities
{
    public class NotaDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty(PropertyName = "id")]
        public int NotaId { get; set; }

        public decimal Nota { get; set; }

        [ForeignKey("UsuarioGanhouNotaId")]
        public virtual UsuarioDomain UsuarioGanhouNota { get; set; }
        public int UsuarioGanhouNotaId { get; set; }
        
        [ForeignKey("UsuarioDeuNotaId")] 
        public virtual UsuarioDomain UsuarioDeuNota { get; set; }
        public int UsuarioDeuNotaId { get; set; }
    }
}