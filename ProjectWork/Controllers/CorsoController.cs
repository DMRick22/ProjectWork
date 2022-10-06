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

        public IActionResult HomeUser()
        {
            return View(DaoCorso.GetInstance().Read());
        }
    }
}
