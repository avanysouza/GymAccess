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
        public class FichaExerciciosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FichaExerciciosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FichaExercicios
        public async Task<IActionResult> Index(int idPlano, int IdAluno, int Id)
        {

            var applicationDbContext = _context.Fichas.Include(f => f.Exercicio).Include(f => f.Plano).Include(f => f.Aluno).Where(m => m.PlanoId == idPlano && m.AlunoId == Id);

            //SELECIONAR ALUNO NA DROP E GUARDAR ID PARA APRESENTAR NA VIEW

            ViewBag.AlunoSelecionado = IdAluno;


            if (IdAluno!= 0)
            {
                Id = IdAluno;
            }

            //Condição para invalidar Id de valor 0 ou menor que zero.
            if (Id <= 0)
            {
                Id = -1;
            }
            
            if(Id == -1)
            {
                ViewBag.Alunos = new SelectList(_context.Alunos.OrderBy(m => m.Nome), "Id", "Nome", "Selecione o Aluno");
            }
            else
            {
                ViewBag.Alunos = new SelectList(_context.Alunos.OrderBy(m => m.Nome), "Id", "Nome", Id);
            }


            //Selecionar dados na Tabela Planos para mostrar na Drop:

            ViewBag.PlanoSelecionado = idPlano;

            ViewBag.Planos = new SelectList(_context.Planos.Where(m => m.AlunoId == Id), "Id", "Nome", idPlano); 


           


            return View(await applicationDbContext.ToListAsync());
        }


        // GET: FichaExercicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fichaExercicio = await _context.Fichas
                .Include(f => f.Aluno)
                .Include(f => f.Exercicio)
                .Include(f => f.Plano)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fichaExercicio == null)
            {
                return NotFound();
            }

            return View(fichaExercicio);
        }

        // GET: FichaExercicios/Create (Novo Plano)
        public IActionResult Create(int? Id, int idPlano)
        {

            //Condição para invalidar Id de valor 0 ou menor que zero.
            if (Id <= 0)
            {
                Id = -1;
            }
           

            if (Id != -1)
            {
                ViewBag.NomeAluno = _context.Alunos.Where(m => m.Id == Id).FirstOrDefault().Nome;
                ViewData["ExercicioId"] = new SelectList(_context.Exercicios, "Id", "Descricao");
                ViewBag.AlunoSelecionado = Id;


                ViewBag.Planos = new SelectList(_context.Planos.Where(m => m.AlunoId == Id), "Id", "Nome", idPlano);
                return View();
            }
            else
            {
                ViewBag.AlunoSelecionado = new SelectList(_context.Alunos, "Id", "Nome");
                ViewData["ExercicioId"] = new SelectList(_context.Exercicios, "Id", "Descricao");
                ViewBag.AlunoSelecionado = Id;


                ViewBag.Planos = new SelectList(_context.Planos.Where(m => m.AlunoId == Id), "Id", "Nome", idPlano);
                return View();

            }


        }


        // POST: FichaExercicios/Create (Novo Plano)
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int Id, int ExercicioId, int PlanoId, string Instrucao)
        {
        
            FichaExercicio m = new FichaExercicio();

            m.AlunoId = Id;
            m.ExercicioId = ExercicioId;
            m.PlanoId = PlanoId; 
            m.Instrucao = Instrucao;
            

            if (ModelState.IsValid)
            {
                _context.Add(m);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.AlunoSelecionado = new SelectList(_context.Alunos, "Id", "Nome", m.AlunoId);
            ViewData["ExercicioId"] = new SelectList(_context.Exercicios, "Id", "Id", m.ExercicioId);
            ViewData["PlanoId"] = new SelectList(_context.Planos, "Id", "Id", m.PlanoId);
            return View(m);
        }

        // GET: FichaExercicios/Create (Novo Exercicio no Plano já existente)

        public IActionResult CreateExercise(int Id, int idPlano)
        {
            ViewBag.AlunoSelecionado = Id;
            ViewBag.PlanoSelecionado = idPlano;

            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Nome", Id);
            ViewData["ExercicioId"] = new SelectList(_context.Exercicios, "Id", "Descricao");
            ViewData["PlanoId"] = new SelectList(_context.Planos, "Id", "Nome"); 

           

            return View();
        }

        // POST: FichaExercicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExercise([Bind("Id,ExercicioId,Instrucao,PlanoId,AlunoId")] FichaExercicio fichaExercicio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fichaExercicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Nome", fichaExercicio.AlunoId);
            ViewData["ExercicioId"] = new SelectList(_context.Exercicios, "Id", "Id", fichaExercicio.ExercicioId);
            ViewData["PlanoSelecionado"] = new SelectList(_context.Planos, "Id", "Nome", fichaExercicio.PlanoId);
            return View(fichaExercicio);
        }


        // GET: FichaExercicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fichaExercicio = await _context.Fichas.FindAsync(id);
            if (fichaExercicio == null)
            {
                return NotFound();
            }
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Nome", fichaExercicio.AlunoId);
            ViewData["ExercicioId"] = new SelectList(_context.Exercicios, "Id", "Id", fichaExercicio.ExercicioId);
            ViewData["PlanoId"] = new SelectList(_context.Planos, "Id", "Id", fichaExercicio.PlanoId);



            return View(fichaExercicio);
        }

        // POST: FichaExercicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,ExercicioId,Instrucao,PlanoId,AlunoId")] FichaExercicio fichaExercicio)
        {
            if (id != fichaExercicio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fichaExercicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FichaExercicioExists(fichaExercicio.Id))
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
            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Nome", fichaExercicio.AlunoId);
            ViewData["ExercicioId"] = new SelectList(_context.Exercicios, "Id", "Id", fichaExercicio.ExercicioId);
            ViewData["PlanoId"] = new SelectList(_context.Planos, "Id", "Id", fichaExercicio.PlanoId);



            return View(fichaExercicio);
        }

        // GET: FichaExercicios/Delete/5
        public async Task<IActionResult> Delete(int? id, int Id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var fichaExercicio = await _context.Fichas
                .Include(f => f.Aluno)
                .Include(f => f.Exercicio)
                .Include(f => f.Plano)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fichaExercicio == null)
            {
                return NotFound();
            }

            return View(fichaExercicio);
        }

        // POST: FichaExercicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var fichaExercicio = await _context.Fichas.FindAsync(id);
            if (fichaExercicio != null)
            {
                _context.Fichas.Remove(fichaExercicio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FichaExercicioExists(int? id)
        {
            return _context.Fichas.Any(e => e.Id == id);
        }
    }
}
