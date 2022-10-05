using Utility;

namespace ProjectWork.Models
{
    public class DaoUtente
    {
        private Database db;

        private static DaoUtente instance = null;

        private DaoUtente()
        {
            db = new Database("ProjectWork");
        }

        public static DaoUtente GetInstance()
        {
            return instance == null ? new DaoUtente() : instance;
        }

        public Utente Cerca(int id)
        {
            string query = $"SELECT * FROM Utenti WHERE id = {id}";

            Dictionary<string, string> riga = db.ReadOne(query);

            Utente u = new Utente();
            u.FromDictionary(riga);

            return u;
        }

        public Utente Cerca(string username)
        {
            string query = $"SELECT * FROM Utenti WHERE username = '{username}'";

            Dictionary<string, string> riga = db.ReadOne(query);

            Utente u = new Utente();
            u.FromDictionary(riga);

            return u;
        }

        public bool Valida(string username, string psw)
        {
            string query = $"SELECT * FROM Utenti " +
                           $"WHERE username = '{username}' " +
                           $"AND psw = HASHBYTES('Sha2_512','{psw}')";

            Dictionary<string, string> riga = db.ReadOne(query);

            if (riga != null)
                return true;
            else
                return false;
        }

        public bool Inserisci(Utente u)
        {
            return db.Update(
                             $"INSERT INTO Utenti (nome,cognome,dob,username,psw,ruolo,oretotali) " +
                             $"VALUES ('{u.Nome}','{u.Cognome}','{u.Dob:yyyy-MM-dd}','{u.Username}', HASHBYTES('Sha2_512','{u.Psw}'),'{u.Ruolo}',0)"
                            );
        }

        public bool Modifica(Utente u)
        {
            Utente un = Cerca(u.Id);

            if (un.Psw != u.Psw || un.Username != u.Username)
            {
                if (un.Username != u.Username)
                {
                    if (Cerca(u.Username) == null)
                    {
                        return db.Update(
                                            $"UPDATE Utenti SET " +
                                            $"username = '{u.Username}', " +
                                            $"WHERE id = {u.Id}"
                                        );
                    }
                }

                if (un.Psw != u.Psw)
                {
                    return db.Update(
                                        $"UPDATE Utenti SET " +
                                        $"psw = HASHBYTES('Sha2_512','{u.Psw}') " +
                                        $"WHERE id = {u.Id}"
                                    );
                }
                return false;
            }
            else
                return false;
        }
    }
}