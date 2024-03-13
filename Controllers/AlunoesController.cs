using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymAcess.Data;
using GymAcess.Models;

namespace GymAcess.Controllers
{
    public class AlunoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlunoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Alunoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Alunos
              .Include(m => m.Instrutor).ToListAsync());
        }

        // GET: Alunoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Alunos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // GET: Alunoes/Create
        public IActionResult Create()
        {

            List<string> status = new List<string>();
            status.Add("Ativo");
            status.Add("Inativo");

            ViewBag.Status = new SelectList(status);

            List<string> perfil = new List<string>();
            perfil.Add("Aluno");
            perfil.Add("Instrutor");

            ViewBag.Perfil = new SelectList(perfil);


            List<string> Sexo = new List<string>();
            Sexo.Add("Feminino");
            Sexo.Add("Masculino");

            ViewBag.Sexo = new SelectList(Sexo);


            ViewBag.Instrutores = new SelectList(_context.Instrutores.ToList(), "Id", "Nome");

            return View();
        }

        // POST: Alunoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Sexo,DataNascimento,Telefone,Morada,Status,Perfil,VencMatri,usuario,Senha,InstrutorId")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aluno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        // GET: Alunoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            List<string> status = new List<string>();
            status.Add("Ativo");
            status.Add("Inativo");

            ViewBag.Status = new SelectList(status);

            List<string> perfil = new List<string>();
            perfil.Add("Aluno");
            perfil.Add("Instrutor");

            ViewBag.Perfil = new SelectList(perfil);

            ViewBag.Instrutores = new SelectList(_context.Instrutores.ToList(), "Id", "Nome");

            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        // POST: Alunoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Nome,Sexo,DataNascimento,Telefone,Morada,Status,Perfil,VencMatri,usuario,Senha,InstrutorId")] Aluno aluno)
        {
            if (id != aluno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aluno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunoExists(aluno.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        // GET: Alunoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Alunos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // POST: Alunoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno != null)
            {
                _context.Alunos.Remove(aluno);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlunoExists(int? id)
        {
            return _context.Alunos.Any(e => e.Id == id);
        }
    }
}
