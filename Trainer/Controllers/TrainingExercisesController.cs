using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trainer.Data;
using Trainer.Models;
using System.Diagnostics;

namespace Trainer.Controllers
{
    public class TrainingExercisesController : Controller
    {
        private readonly TrainingContext _context;

        public TrainingExercisesController(TrainingContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, int page = 1)
        {
            ViewData["CurrentFilter"] = searchString;
            var results = await _context.TrainingExercises.Include(t => t.Exercise).Include(t => t.Training).ThenInclude(t => t.Client).GetPagedAsync(page, 10);

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    results = results.Where(t => t.Training.Client.FirstName.Contains(searchString)
            //                                                || t.Training.Client.LastName.Contains(searchString));
            //}

            return View(results);
        }

        // GET: TrainingExercises
        //public async Task<IActionResult> Index(string searchString)
        //{
        //    ViewData["CurrentFilter"] = searchString;
        //    IQueryable<TrainingExercise> trainingContext = _context.TrainingExercises.Include(t => t.Exercise).Include(t => t.Training).ThenInclude(t => t.Client);

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        trainingContext = trainingContext.Where(t => t.Training.Client.FirstName.Contains(searchString)
        //                                                    || t.Training.Client.LastName.Contains(searchString));
        //    }

        //    return View(await trainingContext.ToListAsync());
        //}

        public async Task<PagedResult<TrainingExercise>> IndexApi(int page = 1)
        {
            return await _context.TrainingExercises.GetPagedAsync(page, 10);
        }

        // GET: TrainingExercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingExercise = await _context.TrainingExercises
                .Include(t => t.Exercise)
                .Include(t => t.Training)
                    .ThenInclude(t => t.Client)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (trainingExercise == null)
            {
                return NotFound();
            }

            return View(trainingExercise);
        }

        // GET: TrainingExercises/Create
        public IActionResult Create()
        {

            ViewData["ExerciseID"] = new SelectList(_context.Exercises, "ExerciseID", "Title");
            ViewData["TrainingID"] = new SelectList(_context.Trainings, "TrainingID", "TrainingInfo");

            return View();
        }

        // POST: TrainingExercises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrainingExerciseID,TrainingID,ExerciseID,Rounds,Repetitions,MaxWeight,Comments")] TrainingExercise trainingExercise)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingExercise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExerciseID"] = new SelectList(_context.Exercises, "ExerciseID", "Title", trainingExercise.ExerciseID);
            ViewData["TrainingID"] = new SelectList(_context.Trainings, "TrainingID", "TrainingID", trainingExercise.TrainingID);
            return View(trainingExercise);
        }

        // GET: TrainingExercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingExercise = await _context.TrainingExercises.FindAsync(id);
            if (trainingExercise == null)
            {
                return NotFound();
            }
            ViewData["ExerciseID"] = new SelectList(_context.Exercises, "ExerciseID", "Title", trainingExercise.ExerciseID);
            ViewData["TrainingID"] = new SelectList(_context.Trainings, "TrainingID", "TrainingID", trainingExercise.TrainingID);
            return View(trainingExercise);
        }

        // POST: TrainingExercises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrainingExerciseID,TrainingID,ExerciseID,Rounds,Repetitions,MaxWeight,Comments")] TrainingExercise trainingExercise)
        {
            if (id != trainingExercise.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingExercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingExerciseExists(trainingExercise.ID))
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
            ViewData["ExerciseID"] = new SelectList(_context.Exercises, "ExerciseID", "Title", trainingExercise.ExerciseID);
            ViewData["TrainingID"] = new SelectList(_context.Trainings, "TrainingID", "TrainingID", trainingExercise.TrainingID);
            return View(trainingExercise);
        }

        // GET: TrainingExercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingExercise = await _context.TrainingExercises
                .Include(t => t.Exercise)
                .Include(t => t.Training)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (trainingExercise == null)
            {
                return NotFound();
            }

            return View(trainingExercise);
        }

        // POST: TrainingExercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingExercise = await _context.TrainingExercises.FindAsync(id);
            _context.TrainingExercises.Remove(trainingExercise);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingExerciseExists(int id)
        {
            return _context.TrainingExercises.Any(e => e.ID == id);
        }
    }
}
