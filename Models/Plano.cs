using System.ComponentModel;

namespace GymAcess.Models
{
    public class Plano
    {
        public int? Id { get; set; }

        [DisplayName("Nome do Plano:")]
        public required string Nome { get; set; }

        [DisplayName("Tipo do Plano")]
        public int TipoId { get; set; }

        [DisplayName("Objetivo do Plano")]
        public int ObjetivoId { get; set; }

        [DisplayName("Aluno")]
        public int? AlunoId { get; set; }

        public virtual Tipo? Tipo { get; set; }

        public virtual Objetivo? Objetivo { get; set; }

        public virtual Aluno? Aluno { get; set; }


    }
}
