using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eaton.Mentoria.Domain.Entities
{
    public class SedeDomain
    {   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public int SedeId {get; set;} 

       public string Nome {get; set;}

       public virtual ICollection<PerfilDomain> Perfis { get; set; } 

        public virtual ICollection<MentoriaDomain> Mentorias { get; set; } 
    }
}