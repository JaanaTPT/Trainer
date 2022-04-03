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
using Trainer.Models.ViewModels;
using Trainer.Services;

namespace Trainer.Controllers
{
    public class TrainingExercisesController : Controller
    {
        private readonly ITrainingExerciseService _trainingExerciseService;
        private const int pagesize = 10;

        public TrainingExercisesController(ITrainingExerciseService trainingExerciseService)
        {
            _trainingExerciseService = trainingExerciseService;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString, int page = 1)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_asc" : "";
            ViewData["CurrentFilter"] = searchString;

            var model = await _trainingExerciseService.GetPagedList(page, pagesize, searchString, sortOrder);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
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
        public async Task<IActionResult> Create()
        {
            var model = new TrainingExerciseEditModel();

            await _trainingExerciseService.FillEditModel(model);

            return View(model);
        }

        // POST: TrainingExercises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrainingExerciseEditModel trainingExercise)
        {
            return await Save(trainingExercise);
        }

        [NonAction]
        private async Task<IActionResult> Save(TrainingExerciseEditModel trainingExercise)
        {
            if (!ModelState.IsValid)
            {
                await _trainingExerciseService.FillEditModel(trainingExercise);

                return View(trainingExercise);
            }

            var response = await _trainingExerciseService.Save(trainingExercise);
            //if (!response.Success)
            //{
            //    AddModelErrors(response);
            //    await _trainingService.FillEditModel(training);

            //    return View(training);
            //}

            return RedirectToAction(nameof(Index));
        }

        // GET: TrainingExercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingExercise = await _trainingExerciseService.GetForEdit(id.Value);
            if (trainingExercise == null)
            {
                return NotFound();
            }
            
            return View(trainingExercise);
        }

        // POST: TrainingExercises/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(TrainingExerciseEditModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _trainingExerciseService.Save(model);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
            }

            return View(model);
        }

        // GET: TrainingExercises/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
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

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
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
