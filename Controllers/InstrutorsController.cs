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
    public class InstrutorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstrutorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Instrutors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Instrutores.ToListAsync());
        }

        // GET: Instrutors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instrutor = await _context.Instrutores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instrutor == null)
            {
                return NotFound();
            }

            return View(instrutor);
        }

        // GET: Instrutors/Create
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

            return View();
        }

        // POST: Instrutors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TituloProf,Nome,DataAdmissao,Telefone,Perfil,Status,Usuario,Senha")] Instrutor instrutor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(instrutor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(instrutor);
        }

        // GET: Instrutors/Edit/5
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

            if (id == null)
            {
                return NotFound();
            }

            var instrutor = await _context.Instrutores.FindAsync(id);
            if (instrutor == null)
            {
                return NotFound();
            }
            return View(instrutor);
        }

        // POST: Instrutors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,TituloProf,Nome,DataAdmissao,Telefone,Perfil,Status,Usuario,Senha")] Instrutor instrutor)
        {
            if (id != instrutor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instrutor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstrutorExists(instrutor.Id))
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
            return View(instrutor);
        }

        // GET: Instrutors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instrutor = await _context.Instrutores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (instrutor == null)
            {
                return NotFound();
            }

            return View(instrutor);
        }

        // POST: Instrutors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var instrutor = await _context.Instrutores.FindAsync(id);
            if (instrutor != null)
            {
                _context.Instrutores.Remove(instrutor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstrutorExists(int? id)
        {
            return _context.Instrutores.Any(e => e.Id == id);
        }
    }
}
