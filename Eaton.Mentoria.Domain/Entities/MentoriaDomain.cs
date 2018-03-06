using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eaton.Mentoria.Domain.Entities
{
    public class MentoriaDomain
    {
        public int MentoriaId { get; set; }
        
        [ForeignKey("MentorId")]
        public MentorDomain Mentores { get; set; }
        public int MentorId { get; set; }
        
        [ForeignKey("CategoriaId")]
        public CategoriaDomain Categorias { get; set; }
        public int CategoriaId { get; set; }
        public Boolean Ativa { get; set; }
        public int MyProperty { get; set; }
        
        [Required]
        [StringLength(20)]
        public string Sede { get; set; }

        public ICollection<AplicacaoDomain> Aplicacoes { get; set; }      

    }
}