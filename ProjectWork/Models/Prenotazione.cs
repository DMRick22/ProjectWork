using Utility;

namespace ProjectWork.Models
{
    public class Prenotazione : Entity
    {
        public Prenotazione() { }

        public DateTime DataPrenotazione { get; set; }
        public int IdUtenti { get; set; }
        public int IdCorsi { get; set; }
    }
}
