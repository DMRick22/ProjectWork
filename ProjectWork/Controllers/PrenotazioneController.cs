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
    }
}
