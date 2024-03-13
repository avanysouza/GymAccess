using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GymAcess.Models
{
    public class Aluno
    {
        public int? Id { get; set; }

        [Required]
        [DisplayName("Nome")]
        public string Nome { get; set; }

        public string Sexo { get; set; }

        [DisplayName("Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        public string Telefone { get; set; }

        public string Morada { get; set; }

        public string Status { get; set; }

        public string Perfil { get; set; }

        [DisplayName("Vencimento da Matrícula")]
        public DateTime VencMatri { get; set; }

        public string? Usuario { get; set; }

        public string? Senha { get; set; }

        [DisplayName("Instrutor")]
        public int InstrutorId { get; set; }

        public virtual Instrutor? Instrutor { get; set; }

        public virtual Administrador? Administrador { get; set; }
    }
}
