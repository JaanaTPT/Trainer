using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trainer.Data;
using Trainer.Models;
using Trainer.Models.TrainingViewModels;

namespace Trainer.Controllers
{
    public class TrainingsController : Controller
    {
        private readonly TrainingContext _context;

        public TrainingsController(TrainingContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id, string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            //var trainings = from t in _context.Trainings
            //                select t;

            var viewModel = new TrainingDetailsData();
            viewModel.Trainings = await _context.Trainings
                  .Include(i => i.Client)
                  .Include(i => i.TrainingExercises)
                    .ThenInclude(i => i.Exercise)
                  .AsNoTracking()
                  .OrderBy(i => i.Date)
                  .ToListAsync();

            if (id != null)
            {
                ViewData["TrainingID"] = id.Value;
                Training training = viewModel.Trainings.Where(
                    i => i.TrainingID == id.Value).Single();
                viewModel.Exercises = training.TrainingExercises.Select(i => i.Exercise);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                viewModel.Trainings = await _context.Trainings
                 .Include(i => i.Client).Where(i => i.Client.FirstName.Contains(searchString) || i.Client.LastName.Contains(searchString))
                 .Include(i => i.TrainingExercises)
                   .ThenInclude(i => i.Exercise)
                 .AsNoTracking()
                 .OrderBy(i => i.Date)
                 .ToListAsync();
            }

            return View(viewModel);
        }


        // GET: Trainings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _context.Trainings
                .Include(t => t.Client)
                .FirstOrDefaultAsync(m => m.TrainingID == id);
            if (training == null)
            {
                return NotFound();
            }

            return View(training);
        }

        // GET: Trainings/Create
        public IActionResult Create()
        {
            ViewData["ClientID"] = new SelectList(_context.Clients, "ID", "FullName");
            return View();
        }

        // POST: Trainings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrainingID,Date,ClientID")] Training training)
        {
            if (ModelState.IsValid)
            {
                _context.Add(training);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientID"] = new SelectList(_context.Clients, "ID", "FullName", training.ClientID);
            return View(training);
        }

        // GET: Trainings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _context.Trainings.FindAsync(id);
            if (training == null)
            {
                return NotFound();
            }
            ViewData["ClientID"] = new SelectList(_context.Clients, "ID", "FirstName", training.ClientID);
            return View(training);
        }

        // POST: Trainings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrainingID,Date,ClientID")] Training training)
        {
            if (id != training.TrainingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(training);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingExists(training.TrainingID))
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
            ViewData["ClientID"] = new SelectList(_context.Clients, "ID", "FirstName", training.ClientID);
            return View(training);
        }

        // GET: Trainings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _context.Trainings
                .Include(t => t.Client)
                .FirstOrDefaultAsync(m => m.TrainingID == id);
            if (training == null)
            {
                return NotFound();
            }

            return View(training);
        }

        // POST: Trainings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var training = await _context.Trainings.FindAsync(id);
            _context.Trainings.Remove(training);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingExists(int id)
        {
            return _context.Trainings.Any(e => e.TrainingID == id);
        }
    }
}
