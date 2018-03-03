using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eaton.Mentoria.Domain.Entities
{
    public class Mentoria
    {
        public int MentoriaId { get; set; }
        
        [ForeignKey("MentorId")]
        public Mentor Mentores { get; set; }
        public int MentorId { get; set; }
        
        [ForeignKey("CategoriaId")]
        public Categoria Categorias { get; set; }
        public int CategoriaId { get; set; }
        public Boolean Ativa { get; set; }
        public int MyProperty { get; set; }
        public string Sede { get; set; }

        public ICollection<Aplicacao> Aplicacoes { get; set; }      

    }
}