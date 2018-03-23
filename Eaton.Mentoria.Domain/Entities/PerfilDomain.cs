using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eaton.Mentoria.Domain.Entities
{
    public class PerfilDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PerfilId{ get; set; }
        
        [ForeignKey("UsuarioId")]
        public UsuarioDomain Usuario { get; set; }
        public int UsuarioId { get; set; }

        [Required]
        [StringLength(150)]
        public string Nome { get; set; }
        
        [Required]
        [StringLength(650)]
        public string MiniBio { get; set; }
        public string Foto { get; set; }
        
        [Required]
        [StringLength(9)]
        public string Cep { get; set; }
        
        [ForeignKey("SedeId")]
        public SedeDomain Sede { get; set; }

        public int SedeId { get; set; }
        
    }
}