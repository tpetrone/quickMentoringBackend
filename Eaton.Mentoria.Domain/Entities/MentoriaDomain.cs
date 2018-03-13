using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eaton.Mentoria.Domain.Entities
{
    public class MentoriaDomain
    
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MentoriaId { get; set; }
        
        [ForeignKey("UsuarioId")]
        public UsuarioDomain Usuario { get; set; }
        public int UsuarioId { get; set; }
        
        [ForeignKey("CategoriaId")]
        public CategoriaDomain Categoria { get; set; }
        public int CategoriaId { get; set; }
        public Boolean Ativa { get; set; }
        
        
        [ForeignKey("SedeId")]
        public SedeDomain Sede { get; set; }

        public int SedeId { get; set; }

        public ICollection<AplicacaoDomain> Aplicacoes { get; set; }
    

    }
}