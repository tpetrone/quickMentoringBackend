using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eaton.Mentoria.Domain.Entities
{
    public class Perfil
    {
        public int PerfilId{ get; set; }
        
        [ForeignKey("UsuarioId")]
        public Usuario Usuarios { get; set; }
        public int UsuarioId { get; set; }
        public string MiniBio { get; set; }
        public string Foto { get; set; }
        public string Cep { get; set; }
        public string sede { get; set; }





    }
}