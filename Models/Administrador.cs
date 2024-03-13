using System.ComponentModel;

namespace GymAcess.Models
{
    public class Administrador
    {
        public int? Id { get; set; }

        
        [DisplayName("Nome")]
        public string Nome { get; set; }

        public string Status { get; set; }


        public string Perfil { get; set; }

        public string? Usuario { get; set; }

        public string? Senha { get; set; }
    }
}
