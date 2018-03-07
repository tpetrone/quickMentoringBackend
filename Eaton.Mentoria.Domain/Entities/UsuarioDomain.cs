using System;
using System.ComponentModel.DataAnnotations;
namespace Eaton.Mentoria.Domain.Entities
{
    public class UsuarioDomain
    {
        public int UsuarioId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        
        [Required]
        [StringLength(20)]
        public string Password { get; set; }
        
        [Required]
        [StringLength(20)]
        public string Role { get; set; }
        public Boolean Ativo { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Sede { get; set; }      
        
        
    }
}