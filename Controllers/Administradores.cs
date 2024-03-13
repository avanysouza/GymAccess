using Microsoft.AspNetCore.Mvc;

namespace GymAcess.Controllers
{
    public class Administradores : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
