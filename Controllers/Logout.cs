using Microsoft.AspNetCore.Mvc;

namespace GymAcess.Controllers
{
    public class Logout : Controller
    {
        public IActionResult Index()
        {

            HttpContext.Session.SetString("Utilizador", "");

            return Redirect("Login");
        }
    }
}
