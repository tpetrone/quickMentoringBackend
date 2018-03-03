using System;
using System.ComponentModel.DataAnnotations;
namespace Eaton.Mentoria.Domain.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public Boolean Ativo { get; set; }
        public string Sede { get; set; }      
        
        
    }
}