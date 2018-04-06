using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Eaton.Mentoria.Domain.Entities
{
    public class PerfilDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty(PropertyName = "id")]
        public int PerfilId{ get; set; }
        
        [ForeignKey("UsuarioId")]
        public virtual UsuarioDomain Usuario { get; set; }

        [JsonProperty(PropertyName = "usuarioId")]
        public int UsuarioId { get; set; }

        [Required]
        [StringLength(150)]
        [JsonProperty(PropertyName = "nome")]
        public string Nome { get; set; }
        
        [Required]
        [StringLength(650)]
        [JsonProperty(PropertyName = "miniBio")]
        public string MiniBio { get; set; }

        [JsonProperty(PropertyName = "foto")]
        public byte[] Foto { get; set; }
        
        [Required]
        [StringLength(9)]
        [JsonProperty(PropertyName = "cep")]
        public string Cep { get; set; }
        
        [ForeignKey("SedeId")]
        [JsonProperty(PropertyName = "sede")]
        public virtual SedeDomain Sede { get; set; }


        [JsonProperty(PropertyName = "sedeId")]
        public int SedeId { get; set; }
        
    }
}