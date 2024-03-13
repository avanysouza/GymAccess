using GymAcess.Data;
using GymAcess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymAcess.Controllers
{
        public class FichaAluno : Controller
        {
            private readonly ApplicationDbContext _context;

            public FichaAluno(ApplicationDbContext context)
            {
                _context = context;
        }
        public async Task<IActionResult> Index()
        {

            var applicationDbContext = _context.Fichas.Include(f => f.Exercicio).Include(f => f.Plano).Include(f => f.Aluno);

            return View(await applicationDbContext.ToListAsync());
        }
    }
}
