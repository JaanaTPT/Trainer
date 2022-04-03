using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Core.IConfiguration;
using Trainer.Data;
using Trainer.Models;
using Trainer.Services;

namespace Trainer.Controllers
{
    public class TrainingExercisesController : Controller
    {
        private readonly ITrainingExerciseService _trainingExerciseService;
        private readonly ITrainingService _trainingService;
        private readonly IExerciseService _exerciseService;

        public TrainingExercisesController(ITrainingExerciseService trainingExerciseService,
                                           ITrainingService trainingService,
                                           IExerciseService exerciseService)
        {
            _trainingExerciseService = trainingExerciseService;
            _trainingService = trainingService;
            _exerciseService = exerciseService;
        }

        public async Task<IActionResult> Index(int? id, string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            IEnumerable<TrainingExercise> results = await _trainingExerciseService.List(searchString);

            if (!String.IsNullOrEmpty(searchString))
            {
                results = results.Where(t => t.Training.Client.FirstName.Contains(searchString)
                                          || t.Training.Client.LastName.Contains(searchString));
            }

            return View(results);
        }


        // GET: TrainingExercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingExercise = await _trainingExerciseService
               .GetById(id.Value);

            if (trainingExercise == null)
            {
                return NotFound();
            }

            return View(trainingExercise);
        }

        // GET: TrainingExercises/Create
        public IActionResult Create()
        {

            ViewData["ExerciseID"] = new SelectList(_exerciseService.DropDownList().OrderBy(e => e.Title), "ID", "Title");
            ViewData["TrainingID"] = new SelectList(_trainingService.DropDownList(), "ID", "ID");

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
                await _trainingExerciseService.Save(trainingExercise);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExerciseID"] = new SelectList(_exerciseService.DropDownList(), "ID", "Title", trainingExercise.ExerciseID);
            ViewData["TrainingID"] = new SelectList(_trainingService.DropDownList(), "ID", "ID", trainingExercise.TrainingID);
            return View(trainingExercise);
        }

        // GET: TrainingExercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingExercise = await _trainingExerciseService.GetById(id.Value);
            if (trainingExercise == null)
            {
                return NotFound();
            }
            ViewData["ExerciseID"] = new SelectList(_exerciseService.DropDownList().OrderBy(e => e.Title), "ID", "Title");
            ViewData["TrainingID"] = new SelectList(_trainingService.DropDownList(), "ID", "ID");
            return View(trainingExercise);
        }

        // POST: TrainingExercises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var trainingExerciseToUpdate = await _trainingExerciseService.GetById(id.Value);

            if (await TryUpdateModelAsync<TrainingExercise>(
                trainingExerciseToUpdate,
                "",
                t => t.TrainingID, t => t.ExerciseID, t => t.Rounds, t => t.Repetitions, t => t.MaxWeight, t => t.Comments));
            {
                try
                {
                    await _trainingExerciseService.Save(trainingExerciseToUpdate);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: TrainingExercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingExercise = await _trainingExerciseService.GetById(id.Value);

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
            var trainingExercise = await _trainingExerciseService.GetById(id);

            if (trainingExercise == null)
            {
                return NotFound();
            }

            try
            {
                await _trainingExerciseService.Delete(trainingExercise);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool TrainingExerciseExists(int id)
        {
            return _trainingExerciseService.GetById(id) != null;
        }
    }
}
