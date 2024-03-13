using System.ComponentModel;

namespace GymAcess.Models
{
    public class Objetivo
    {

        public int? Id { get; set; }

        [DisplayName("Objetivo")]
        public string Objetivos { get; set; }
    }
}
