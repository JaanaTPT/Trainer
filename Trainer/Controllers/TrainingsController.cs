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
using Trainer.Models.TrainingViewModels;
using Trainer.Models.ViewModels;
using Trainer.Services;

namespace Trainer.Controllers
{
    public class TrainingsController : Controller
    {
        private readonly ITrainingService _trainingService;
        private const int pagesize = 10;

        public TrainingsController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString, int page = 1)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_asc" : "";
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "fullName_asc" : "";
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
        public IActionResult Create()
        {
            var model = new TrainingEditModel();

            return View(model);
        }

        // POST: Trainings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrainingEditModel training)
        {
            return await Save(training);
        }

        [NonAction]
        private async Task<IActionResult> Save(TrainingEditModel training)
        {
            var response = await _trainingService.Save(training);
            //if (!response.Success)
            //{
            //    AddModelErrors(response);

            //    return View(client);
            //}

            return RedirectToAction(nameof(Index));
        }

        // GET: Trainings/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

            //ViewData["ClientID"] = new SelectList(_trainingService.DropDownList());
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
                //Log the error (uncomment ex variable name and write a log.)
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
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool TrainingExists(int id)
        {
            return _trainingService.GetById(id) != null;
        }
    }
}
