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
    public class PlanoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlanoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Planoes
        public async Task<IActionResult> Index(int dropAluno)
        {
            ViewBag.Alunos = new SelectList(_context.Alunos, "Id", "Nome");

            var applicationDbContext = _context.Planos.Include(p => p.Aluno).Include(p => p.Objetivo).Include(p => p.Tipo).Where(m => m.AlunoId == dropAluno);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Planoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plano = await _context.Planos
                .Include(p => p.Aluno)
                .Include(p => p.Objetivo)
                .Include(p => p.Tipo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plano == null)
            {
                return NotFound();
            }

            return View(plano);
        }

        // GET: Planoes/Create
        public IActionResult Create()
        {
            
            ViewBag.Aluno = new SelectList(_context.Alunos.ToList(), "Id", "Nome");
            ViewBag.Tipo = new SelectList(_context.Tipos.ToList(), "Id", "Tipos");
            ViewBag.Objetivo = new SelectList(_context.Objetivos.ToList(), "Id", "Objetivos");


            return View();
        }

        // POST: Planoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,TipoId,ObjetivoId,AlunoId")] Plano plano)
        {

            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Nome", plano.AlunoId);
            ViewData["ObjetivoId"] = new SelectList(_context.Objetivos, "Id", "Objetivos", plano.ObjetivoId);
            ViewData["TipoId"] = new SelectList(_context.Tipos, "Id", "Tipos", plano.TipoId);

            if (ModelState.IsValid)
            {
                _context.Add(plano);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(plano);
        }

        // GET: Planoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plano = await _context.Planos.FindAsync(id);
            if (plano == null)
            {
                return NotFound();
            }
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Nome", plano.AlunoId);
            ViewData["ObjetivoId"] = new SelectList(_context.Objetivos, "Id", "Objetivos", plano.ObjetivoId);
            ViewData["TipoId"] = new SelectList(_context.Tipos, "Id", "Tipos", plano.TipoId);
            return View(plano);
        }

        // POST: Planoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Nome,TipoId,ObjetivoId,AlunoId")] Plano plano)
        {
            if (id != plano.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plano);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanoExists(plano.Id))
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
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Nome", plano.AlunoId);
            ViewData["ObjetivoId"] = new SelectList(_context.Objetivos, "Id", "Id", plano.ObjetivoId);
            ViewData["TipoId"] = new SelectList(_context.Tipos, "Id", "Id", plano.TipoId);
            return View(plano);
        }

        // GET: Planoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plano = await _context.Planos
                .Include(p => p.Aluno)
                .Include(p => p.Objetivo)
                .Include(p => p.Tipo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plano == null)
            {
                return NotFound();
            }

            return View(plano);
        }

        // POST: Planoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var plano = await _context.Planos.FindAsync(id);
            if (plano != null)
            {
                _context.Planos.Remove(plano);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanoExists(int? id)
        {
            return _context.Planos.Any(e => e.Id == id);
        }
    }
}
