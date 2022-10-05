using Utility;

namespace ProjectWork.Models
{
    public class Utente : Entity
    {
        public Utente() { }

        public string Nome { get; set; }
        public string Cognome { get; set; }
        public DateTime Dob { get; set; }
        public string Username { get; set; }
        public string Psw { get; set; }
        public string Ruolo { get; set; }
        public int OreTotali { get; set; }
    }
}
