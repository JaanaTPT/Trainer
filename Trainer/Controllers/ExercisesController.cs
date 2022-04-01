using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trainer.Core.IConfiguration;
using Trainer.Data;
using Trainer.Models;
using Trainer.Services;

namespace Trainer.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly IExerciseService _exerciseService;

        public ExercisesController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        // GET: Exercises
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["MuscleSortParm"] = sortOrder == "Muscle" ? "muscle_desc" : "Muscle";
            ViewData["CurrentFilter"] = searchString;
            IEnumerable<Exercise> exercises = await _exerciseService.List();

            if (!String.IsNullOrEmpty(searchString))
            {
                exercises = exercises.Where(e => e.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    exercises = exercises.OrderByDescending(e => e.Title);
                    break;
                case "Muscle":
                    exercises = exercises.OrderBy(e => e.MuscleGroup);
                    break;
                case "muscle_desc":
                    exercises = exercises.OrderByDescending(e => e.MuscleGroup);
                    break;
                default:
                    exercises = exercises.OrderBy(e => e.Title);
                    break;
            }

            return View(exercises.ToList());
        }

        // GET: Exercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _exerciseService.GetById(id.Value);

            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // GET: Exercises/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExerciseID,Title,MuscleGroup")] Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                await _exerciseService.Save(exercise);
                //await _exerciseService.CommitAsync();
                return RedirectToAction(nameof(Index));
            }

            //var statuses = from MuscleGroup s in Enum.GetValues(typeof(MuscleGroup))
            //               select new {Name = s.ToString() };
            //ViewData["MuscleGroup"] = new SelectList(statuses);

            return View(exercise);
        }

        // GET: Exercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _exerciseService.GetById(id.Value);
            if (exercise == null)
            {
                return NotFound();
            }
            return View(exercise);
        }

        // POST: Exercises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id, [Bind("ExerciseID,Title,MuscleGroup")] Exercise exercise)
        {
            if (id == null)
            {
                return NotFound();
            }
            var exerciseToUpdate = await _exerciseService.GetById(id.Value);
            if (await TryUpdateModelAsync<Exercise>(
                exerciseToUpdate,
                "",
                e => e.Title, e => e.MuscleGroup))
            {
                try
                {
                    await _exerciseService.Save(exerciseToUpdate);
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
            return View(exerciseToUpdate);
        }


        // GET: Exercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _exerciseService.GetById(id.Value);

            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exercise = await _exerciseService.GetById(id);
            if (exercise == null)
            {
                return NotFound();
            }

            try
            {
                await _exerciseService.Delete(exercise);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool ExerciseExists(int id)
        {
            return _exerciseService.GetById(id) != null;
        }
    }
}
