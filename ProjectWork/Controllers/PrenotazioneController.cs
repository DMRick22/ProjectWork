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
            Prenotazione p = new Prenotazione();
            p.DataPrenotazione = DateTime.Today;
            p.IdUtenti = LoginController.utenteLoggato.Id;
            p.IdCorsi = id;

            if (DaoPrenotazione.GetInstance().InsertOrdine(p, LoginController.utenteLoggato.Id))
            {
                return Redirect("/Corso/HomeUser");
            }
            return Content($"Attenzione non è stato possibile procedere con l'inserimento della prenotazione corso con ID {id}.\n Contattare l'amministratore");
        }
    }
}
