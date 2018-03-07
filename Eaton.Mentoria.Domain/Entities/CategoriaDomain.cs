using System.ComponentModel.DataAnnotations;
namespace Eaton.Mentoria.Domain.Entities
{
    public class CategoriaDomain
    {
        public int CategoriaId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string CategoriaA { get; set; }
        
        [Required]
        [StringLength(100)]
        public string CategoriaB { get; set; }
        
        [Required]
        [StringLength(100)]
        public string CategoriaC { get; set; }
                

    }
}