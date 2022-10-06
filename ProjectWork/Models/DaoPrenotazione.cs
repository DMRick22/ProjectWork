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
    }
}
