using Microsoft.AspNetCore.Mvc;
using ProjectWork.Models;

namespace ProjectWork.Controllers
{
    public class PrenotazioneController : Controller
    {
        public IActionResult ElencoOrdiniUtente()
        {
            int idutenti = LoginController.utenteLoggato.Id;
            return View(DaoPrenotazione.GetInstance().IMieiOrdini(idutenti));
        }

        public IActionResult ElencoOrdiniAmministratore()
        {
            return View(DaoPrenotazione.GetInstance().Read());
        }

        public IActionResult NuovaPrenotazione(int id)
        {
            int utenteLoggato = LoginController.utenteLoggato.Id;
            Prenotazione p = new Prenotazione();
            p.DataPrenotazione = DateTime.Today;
            p.IdUtenti = utenteLoggato;
            p.IdCorsi = id;

            if (DaoPrenotazione.GetInstance().ControllaCorso(utenteLoggato, id))
            {
                if (DaoPrenotazione.GetInstance().InsertOrdine(p, utenteLoggato))
                {
                    if (DaoPrenotazione.GetInstance().AggiungiOre(utenteLoggato, id))
                    {
                        return Redirect("/Corso/HomeUser");
                    }
                }
            }
            return Redirect($"/Corso/HomeUserAlert");
        }

        public IActionResult ErrorUser()
        {
            return View();
        }

        public IActionResult ErrorTerzo()
        {
            return View();
        }

        public IActionResult ErrorAdmin()
        {
            return View();
        }

        public IActionResult EliminaPrenotazione(int id)
        {
            if (DaoPrenotazione.GetInstance().RimuoviOreDopoEliminazione(id) && DaoPrenotazione.GetInstance().Delete(id))
            {
                return Redirect("/Prenotazione/ElencoOrdiniAmministratore");
            }
            else
                return Content("/Prenotazione/ErrorAdmin");
        }

        public IActionResult EliminaPrenotazioneUser(int id)
        {
            if (DaoPrenotazione.GetInstance().RimuoviOreDopoEliminazione(id) && DaoPrenotazione.GetInstance().Delete(id))
            {
                return Redirect("/Prenotazione/ElencoOrdiniUtente");
            }
            else
                return Content("/Prenotazione/ErrorAdmin");
        }
    }
}
