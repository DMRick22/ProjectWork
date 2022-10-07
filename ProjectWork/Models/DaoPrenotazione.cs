using Utility;

namespace ProjectWork.Models
{
    public class DaoPrenotazione
    {
        private Database db;

        private static DaoPrenotazione instance = null;

        private DaoPrenotazione()
        {
            db = new Database("ProjectWork");
        }

        public static DaoPrenotazione GetInstance()
        {
            return instance == null ? new DaoPrenotazione() : instance;
        }

        public List<Entity> Read()
        {
            List<Entity> ris = new List<Entity>();

            List<Dictionary<string, string>> tabella = db.Read("SELECT * FROM Prenotazioni");

            foreach (Dictionary<string, string> riga in tabella)
            {
                Prenotazione p = new Prenotazione();
                p.FromDictionary(riga);

                ris.Add(p);
            }
            return ris;
        }

        public List<Entity> IMieiOrdini(int idutenti)
        {
            List<Entity> ris = new List<Entity>();

            List<Dictionary<string, string>> tabella = db.Read($"SELECT * FROM Prenotazioni where idUtenti = {idutenti}");
            
            foreach (Dictionary<string, string> riga in tabella)
            {
                Prenotazione p = new Prenotazione();
                p.FromDictionary(riga);

                ris.Add(p);
            }
            return ris;
        }

        public bool Delete(int id)
        {
            return db.Update($"DELETE FROM Prenotazioni WHERE id = {id}");
        }
        
        public bool DeleteAfterCourse(int id)
        {
            return db.Update($"DELETE FROM Prenotazioni WHERE idCorsi = {id}");
        }

        public bool InsertOrdine(Entity e, int utente)
        {
            return db.Update($"INSERT INTO Prenotazioni " +
                             $"(dataprenotazione,idutenti,idcorsi) " +
                             $"VALUES " +
                             $"('{((Prenotazione)e).DataPrenotazione:yyyy-MM-dd}'," +
                             $"{utente},{((Prenotazione)e).IdCorsi})");
        }

        public bool AggiungiOre(int idutenti, int idcorsi)
        {
            Corso c = new Corso();

            List<Dictionary<string, string>> tabella = db.Read($"SELECT * FROM Corsi WHERE id = {idcorsi}");

            foreach (Dictionary<string, string> riga in tabella)
            {
                c.FromDictionary(riga);
            }

            Utente u = new Utente();

            List<Dictionary<string, string>> tabella2 = db.Read($"SELECT * FROM Utenti WHERE id = {idutenti}");

            foreach (Dictionary<string, string> riga in tabella2)
            {
                u.FromDictionary(riga);
            }
            int oreTotali = 0;

            oreTotali = (u.OreTotali + c.Durata);

            return db.Update($"UPDATE Utenti SET oreTotali = {oreTotali} WHERE id = {idutenti}");
        }

        public bool ControllaCorso(int idutenti, int idcorsi)
        {
            Prenotazione p = new Prenotazione();

             List<Dictionary<string, string>> tabella = db.Read($"SELECT * FROM Prenotazioni WHERE idUtenti = {idutenti} AND idCorsi = {idcorsi}");

            if (tabella.Count == 0)
                return true;
            else
                return false;
        }
    }
}
