using System.ComponentModel;

namespace GymAcess.Models
{
    public class Utilizador
    {
        public int? Id { get; set; }

        public string Status { get; set; }

        public string Perfil { get; set; }

        public string? Usuario { get; set; }

        public string? Senha { get; set; }
        public virtual Instrutor? Instrutor { get; set; }
        public virtual Aluno? Aluno { get; set; }
        public virtual Administrador? Administrador { get; set; }

    }
}
