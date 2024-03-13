
using System.ComponentModel.DataAnnotations.Schema;

namespace GymAcess.Models
{
    public class FichaExercicio
    {
        
        public int? Id { get; set; }

        public int ExercicioId { get; set; }

        public string Instrucao { get; set; }

        public int PlanoId { get; set; }

        public int AlunoId { get; set; }
        public virtual Plano? Plano { get; set; }

        public virtual Exercicio? Exercicio { get; set; }

        public virtual Aluno? Aluno { get; set; }
    }
}
