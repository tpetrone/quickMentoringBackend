using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eaton.Mentoria.Domain.Entities
{
    public class CategoriaDomain
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoriaId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        public ICollection<MentoriaDomain> Mentorias { get; set; }

        
        
       
                

    }
}