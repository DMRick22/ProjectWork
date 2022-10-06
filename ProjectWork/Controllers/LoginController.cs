using Microsoft.AspNetCore.Mvc;
using ProjectWork.Models;

namespace ProjectWork.Controllers
{
    public class LoginController : Controller
    {
        private ILogger<LoginController> il;

        public static Utente utenteLoggato = null;

        private static int chiamata = -1;


        public LoginController(ILogger<LoginController> l)
        {
            il = l;
        }

        public IActionResult Index()
        {
            chiamata++;

            il.LogInformation($"TENTATIVO NUMERO: {chiamata}");

            return View(chiamata);
        }

        public IActionResult Valida(Dictionary<string, string> parametri)
        {
            if (DaoUtente.GetInstance().Valida(parametri["username"], parametri["psw"]))
            {
                il.LogInformation($"UTENTE LOGGATO: {parametri["username"]}");


                utenteLoggato = DaoUtente.GetInstance().Cerca(parametri["username"]);

                if (utenteLoggato.Ruolo.ToLower() == "amministratore")
                {
                    return Redirect("/Corso/HomeAdmin");
                }
                if (utenteLoggato.Ruolo.ToLower() == "terzo")
                {
                    return Redirect("/Corso/HomeTerzo");
                }
                return Redirect("/Corso/HomeUser");
            }
            else
                return Redirect("Index");
        }

        public IActionResult Logout()
        {
            chiamata = -1;
            il.LogInformation($"LOGOUT: {utenteLoggato.Username}");
            utenteLoggato = null;

            return Redirect("Index");
        }

        public IActionResult Registrazione()
        {
            return View();
        }

        public IActionResult Salva(Dictionary<string, string> parametri)
        {
            Utente u = new Utente();
            u.FromDictionary(parametri);
            u.Ruolo = "Utente";
            chiamata--;

            if (DaoUtente.GetInstance().Inserisci(u))
                return Redirect("/Login/Index");
            else
                return Content("Registrazione fallita. Rincontrollare i dati inseriti e riprovare");
        }
    }
}
