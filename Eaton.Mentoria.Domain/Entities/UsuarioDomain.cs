using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eaton.Mentoria.Domain.Entities
{
    public class UsuarioDomain
    //Usuario é definida como a classe pai das classes filhas: mentor e mentorado. Usuário é uma tabela
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        public ICollection<HashesDomain> Hashes { get; set; } 
        
        public ICollection<AplicacaoDomain> Aplicacoes { get; set; } 
        public ICollection<MentoriaDomain> Mentorias { get; set; }
        public ICollection<NotaDomain> Notas { get; set; }


    }
}