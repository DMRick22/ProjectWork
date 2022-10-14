using ProjectWork.Controllers;
using Utility;

namespace ProjectWork.Models
{
    public class DaoCorso
    {
        private Database db;

        private static DaoCorso instance = null;

        private DaoCorso()
        {
            db = new Database("ProjectWork");
        }

        public static DaoCorso GetInstance()
        {
            return instance == null ? new DaoCorso() : instance;
        }

        public List<Entity> Read()
        {
            List<Entity> ris = new List<Entity>();

            List<Dictionary<string, string>> tabella = db.Read("SELECT * FROM Corsi order by id desc");

            int idutenti = LoginController.utenteLoggato.Id;

            foreach (Dictionary<string, string> riga in tabella)
            {
                Corso c = new Corso();
                c.FromDictionary(riga);

                c.Prenotabile = (DaoPrenotazione.GetInstance().ControllaCorso(idutenti, c.Id));

                ris.Add(c);
            }
            return ris;
        }

        //Il readlike viene utilizzato per ricercare il valore imposto nella barra di ricerca, effettuerà due query, una per autore e una per linguaggiotrattato
        public List<Entity> ReadLike(string valore)
        {
            List<Entity> ris = new List<Entity>();

            List<Dictionary<string, string>> tabella = db.Read($"SELECT * FROM Corsi WHERE autore LIKE '%{valore}%'");

            foreach (Dictionary<string, string> riga in tabella)
            {
                Corso c = new Corso();
                c.FromDictionary(riga);

                ris.Add(c);
            }

            List<Dictionary<string, string>> tabella2 = db.Read($"SELECT * FROM Corsi WHERE linguaggiotrattato LIKE '%{valore}%'");

            foreach (Dictionary<string, string> riga in tabella2)
            {
                Corso c = new Corso();
                c.FromDictionary(riga);

                ris.Add(c);
            }

            return ris;
        }

        public bool Delete(int id)
        {
            return db.Update($"DELETE FROM Corsi WHERE id = {id}");
        }

        public bool Insert(Entity e)
        {
            return db.Update($"INSERT INTO Corsi " +
                             $"(copertina,nome,autore,linguaggiotrattato,durata,numerolezioni,costo,categoria,descrizione) " +
                             $"VALUES " +
                             $"('{((Corso)e).Copertina}' , '{((Corso)e).Nome}', " +
                             $"'{((Corso)e).Autore}', '{(((Corso)e).LinguaggioTrattato)}',{((Corso)e).Durata},{((Corso)e).NumeroLezioni},{((Corso)e).Costo},'{((Corso)e).Categoria}','{(((Corso)e).Descrizione)}')");
        }

        public bool Update(Entity e)
        {

            return db.Update(
                             $"UPDATE Corsi SET " +
                             $"copertina = '{((Corso)e).Copertina}', " +
                             $"nome = '{((Corso)e).Nome}', " +
                             $"autore = '{((Corso)e).Autore}', " +
                             $"linguaggiotrattato = '{((Corso)e).LinguaggioTrattato}', " +
                             $"durata = {((Corso)e).Durata}, " +
                             $"numerolezioni = {((Corso)e).NumeroLezioni}, " +
                             $"costo = {((Corso)e).Costo} " +
                             $"WHERE id = {e.Id}"
                             );
        }

        public Entity Find(int id)
        {
            foreach (Entity e in Read())
                if (e.Id == id)
                    return e;

            return null;
        }

    }
}
