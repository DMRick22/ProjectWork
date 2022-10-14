using ProjectWork.Controllers;
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

            foreach (Prenotazione p in ris)
            {
                Dictionary<string, string> tab = db.ReadOne($" SELECT nome from corsi where id = {p.IdCorsi}");
                p.Nome = tab["nome"];
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

        //IL METODO VA A CREARE I 3 OGGETTI, NELLA PRIMA PARTE SONO NECESSARI PER RECUPERARE LE INFORMAZIONI, UNA VOLTA RECUPERARE INFO DI CORSO E UTENTE,
        //SI PROCEDE A SOTTRARRE LA DURATA DI QUEL CORSO AL TOTALE ORE DI QUEL UTENTE, IN CASO DI SUCCESSO SI PASSERà AL SUCCESSIVO METODO NEL PrenotazioneController
        //E SI ELIMINERà LA PRENOTAZIONE ASSOCIATA

        public bool RimuoviOreDopoEliminazione(int idprenotazione)
        {
            Prenotazione p = new Prenotazione();

            List<Dictionary<string, string>> tabellaPrenotazione = db.Read($"SELECT * FROM Prenotazioni WHERE id = {idprenotazione}");

            foreach (Dictionary<string, string> riga in tabellaPrenotazione)
            {
                p.FromDictionary(riga);
            }

            Corso c = new Corso();

            List<Dictionary<string, string>> tabella = db.Read($"SELECT * FROM Corsi WHERE id = {p.IdCorsi}");

            foreach (Dictionary<string, string> riga in tabella)
            {
                c.FromDictionary(riga);
            }

            Utente u = new Utente();

            List<Dictionary<string, string>> tabella2 = db.Read($"SELECT * FROM Utenti WHERE id = {p.IdUtenti}");

            foreach (Dictionary<string, string> riga in tabella2)
            {
                u.FromDictionary(riga);
            }

            int oreAggiornate = 0;
            oreAggiornate = (u.OreTotali - c.Durata);

            if (db.Update($"UPDATE Utenti SET oreTotali = {oreAggiornate} WHERE id = {p.IdUtenti}"))
                return true;
            else
                return false;
        }


        public Prenotazione PrenotazioneFromCorso(int idutenti, int idcorsi)
        {

            Dictionary<string, string> tabella = db.ReadOne($"SELECT * FROM Prenotazioni WHERE idUtenti = {idutenti} AND idCorsi = {idcorsi}");

            Prenotazione p = new Prenotazione();
            p.FromDictionary(tabella);

            return p;
        }
    }
}
