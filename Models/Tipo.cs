using System.ComponentModel;

namespace GymAcess.Models
{
    public class Tipo
    {

        public int? Id { get; set; }

        [DisplayName("Tipo")]
        public required string Tipos { get; set; }
    }
}
