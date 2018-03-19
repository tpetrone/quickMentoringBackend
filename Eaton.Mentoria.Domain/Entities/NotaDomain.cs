using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eaton.Mentoria.Domain.Entities
{
    public class NotaDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotaId { get; set; }

        public decimal Nota { get; set; }

        [ForeignKey("UsuarioGanhouNotaId")]
        public UsuarioDomain UsuarioGanhouNota { get; set; }
        public int UsuarioGanhouNotaId { get; set; }
        
        [ForeignKey("UsuarioDeuNotaId")] 
        public UsuarioDomain UsuarioDeuNota { get; set; }
        public int UsuarioDeuNotaId { get; set; }
    }
}