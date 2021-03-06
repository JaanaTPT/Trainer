using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Trainer.Models.ViewModels;
using Trainer.Services;

namespace Trainer.Controllers
{
    public class TrainingsController : BaseController
    {
        private readonly ITrainingService _trainingService;
        private const int pagesize = 10;

        public TrainingsController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString, int page = 1)
        {
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_asc" : "";
            ViewData["NameSortParm"] = sortOrder == "fullName_asc" ? "fullName_desc" : "fullName_asc";
            ViewData["CurrentFilter"] = searchString;

            var model = await _trainingService.GetPagedList(page, pagesize, searchString, sortOrder);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }


        // GET: Trainings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _trainingService
               .GetById(id.Value);

            if (training == null)
            {
                return NotFound();
            }

            return View(training);
        }

        // GET: Trainings/Create
        public async Task<IActionResult> Create()
        {
            var model = new TrainingEditModel();

            await _trainingService.FillEditModel(model);

            return View(model);
        }

        // POST: Trainings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrainingEditModel training)
        {
            return await Save(training);
        }

        [NonAction]
        private async Task<IActionResult> Save(TrainingEditModel training)
        {
            if (!ModelState.IsValid)
            {
                await _trainingService.FillEditModel(training);

                return View(training);
            }

            var response = await _trainingService.Save(training);
            if (!response.Success)
            {
                AddModelErrors(response);
                await _trainingService.FillEditModel(training);

                return View(training);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Trainings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _trainingService.GetForEdit(id.Value);
            if (training == null)
            {
                return NotFound();
            }

            return View(training);
        }

        // POST: Trainings/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(TrainingEditModel model)
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
                await _trainingService.Save(model);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
            }

            return View(model);
        }

        // GET: Trainings/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _trainingService.GetById(id.Value);

            if (training == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(training);
        }

        // POST: Trainings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var training = await _trainingService.GetById(id);
            if (training == null)
            {
                return NotFound();
            }

            try
            {
                await _trainingService.Delete(training);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool TrainingExists(int id)
        {
            return _trainingService.GetById(id) != null;
        }
    }
}
