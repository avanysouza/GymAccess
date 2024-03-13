using GymAcess.Data;
using GymAcess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymAcess.Controllers
{
    public class Login : Controller
    {

        private readonly ApplicationDbContext _context;

        public Login(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string? nomeUsuario, string? senha)
        {

            if (HttpContext.Session.GetString("Controlador") == null)
            {
                HttpContext.Session.SetString("Controlador", "Home");
            }
                

            Utilizador user = new Utilizador();

            user = _context.Utilizadores.FirstOrDefault(m => m.Usuario == nomeUsuario && m.Senha == senha);


            if(user == null)
            {
                HttpContext.Session.SetString("Utilizador", "");
                HttpContext.Session.SetString("Controlador", "Home");
                return View();
            }
            else
            {
                if(user.Status != "Ativo")
                {
                    HttpContext.Session.SetString("Utilizador", "");
                    HttpContext.Session.SetString("Status", "");
                    return Redirect("~/Login/Index");

                }
                if (user.Status == "Ativo" && user.Perfil == "Aluno")
                {
                    HttpContext.Session.SetString("Utilizador", user.Usuario.ToString());
                    HttpContext.Session.SetString("Administrador", "Nao");
                    HttpContext.Session.SetString("Status", "Ativo");
                    HttpContext.Session.SetString("Instrutor", "Nao");
                    HttpContext.Session.SetString("Aluno", "Sim");
                    return Redirect("~/" + HttpContext.Session.GetString("/Home"));
                }
                if (user.Status == "Ativo" && user.Perfil == "Administrador")
                {
                    HttpContext.Session.SetString("Utilizador", user.Usuario.ToString());
                    HttpContext.Session.SetString("Administrador", "Sim");
                    HttpContext.Session.SetString("Instrutor", "Nao");
                    HttpContext.Session.SetString("Status", "Ativo");
                    HttpContext.Session.SetString("Aluno", "Nao");

                    return Redirect("~/" + HttpContext.Session.GetString(("Controlador") + "/Home"));
                }
                if (user.Status == "Ativo" && user.Perfil == "Instrutor")
                {
                    HttpContext.Session.SetString("Utilizador", user.Usuario.ToString());
                    HttpContext.Session.SetString("Administrador", "Nao");
                    HttpContext.Session.SetString("Instrutor", "Sim");
                    HttpContext.Session.SetString("Status", "Ativo");
                    HttpContext.Session.SetString("Aluno", "Nao");
                    return Redirect("~/" + HttpContext.Session.GetString(("Controlador") + "/Home"));
                }
            }
          
            return View();
        }
    }
}
