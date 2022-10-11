using Microsoft.AspNetCore.Mvc;
using Utility;
using ProjectWork.Models;

namespace ProjectWork.Controllers
{
    public class CorsoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult HomeAdmin()
        {
            return View(DaoCorso.GetInstance().Read());
        }

        public IActionResult HomeAdminLike(string valore)
        {
            return View(DaoCorso.GetInstance().ReadLike(valore));
        }

        public IActionResult HomeUser()
        {
            return View(DaoCorso.GetInstance().Read());
        }

        public IActionResult HomeUserLike(string valore)
        {
            return View(DaoCorso.GetInstance().ReadLike(valore));
        }

        public IActionResult HomeTerzo()
        {
            return View(DaoCorso.GetInstance().Read());
        }

        public IActionResult HomeTerzoLike(string valore)
        {
            return View(DaoCorso.GetInstance().ReadLike(valore));
        }

        public IActionResult Elimina(int id)
        {
            //VIENE FATTO UNA PRIMA ELIMINAZIONE DELLE PRENOTAZIONI, DOVE L'IDCORSO CORRISPONDE ALLA PRENOTAZIONE, UNA VOLTA ELIMINATI TUTTI GLI ORDINI, SI PROCEDE
            //AD ELIMINARE IL CORSO STESSO
            if (DaoCorso.GetInstance().Delete(id))
            {
                if (DaoPrenotazione.GetInstance().DeleteAfterCourse(id))
                {
                    return Redirect("/Corso/HomeAdmin");
                }
                return Redirect("/Corso/HomeAdmin");
            }
            return Redirect("/Prenotazione/ErrorAdmin");
        }

        public IActionResult NuovoCorso(Dictionary<string, string> parametri)
        {
            Corso c = new Corso();
            c.FromDictionary(parametri);

                if (DaoCorso.GetInstance().Insert(c))
                {
                    return Redirect("/Corso/HomeTerzo");
                }
                else
                    return Redirect("/Prenotazione/ErrorTerzo");
        }

        public IActionResult IMieiDettagli()
        {
            return View(DaoUtente.GetInstance().Cerca(LoginController.utenteLoggato.Id));
        }

        public IActionResult ContattiUser()
        {
            return View();
        }

        public IActionResult ContattiAdmin()
        {
            return View();
        }

        public IActionResult ContattiTerzo()
        {
            return View();
        }

        public IActionResult DettagliCorsoAdmin(int id)
        {
            return View(DaoCorso.GetInstance().Find(id));
        }

        public IActionResult DettagliCorsoUser(int id)
        {
            return View(DaoCorso.GetInstance().Find(id));
        }

        public IActionResult FormModifica(int id)
        {
            Corso c = (Corso)DaoCorso.GetInstance().Find(id);

            return View(c);
        }

        public IActionResult ModificaProdotto(Dictionary<string, string> parametri)
        {
            Corso c = new Corso();
            c.FromDictionary(parametri);

            if (DaoCorso.GetInstance().Update(c))
                return Redirect("/Corso/HomeTerzo");
            else
                return Redirect("/Prenotazione/ErrorTerzo");
        }
    }
}
