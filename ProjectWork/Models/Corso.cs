using Utility;

namespace ProjectWork.Models
{
    public class Corso : Entity
    {
        public Corso() { }

        public string Copertina { get; set; }
        public string Nome { get; set; }
        public string Autore { get; set; }
        public string LinguaggioTrattato { get; set; }
        public int Durata { get; set; }
        public int NumeroLezioni { get; set; }
        public double Costo { get; set; }
        public string Categoria { get; set; }
        public string Descrizione { get; set; }
    }
}
