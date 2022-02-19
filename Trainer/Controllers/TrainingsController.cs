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

namespace Trainer.Controllers
{
    public class TrainingsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrainingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int? id, string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var viewModel = new TrainingDetailsData();

            viewModel.Trainings = await _unitOfWork.TrainingRepository.List(searchString);

            if (id != null)
            {
                ViewData["TrainingID"] = id.Value;

                var training = viewModel.Trainings.FirstOrDefault(t => t.ID == id.Value);
                if(training != null)
                {
                    viewModel.Exercises = training.TrainingExercises.Select(te => te.Exercise);
                }
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

            var training = await _unitOfWork.TrainingRepository
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
            ViewData["ClientID"] = new SelectList(_unitOfWork.ClientRepository.DropDownList().OrderBy(c => c.FullName), "ID", "FullName");
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
                await _unitOfWork.TrainingRepository.Save(training);
                await _unitOfWork.CommitAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ClientID"] = new SelectList(_unitOfWork.TrainingRepository.Clients, "ID", "FullName", training.ClientID);
            return View(training);
        }

        // GET: Trainings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _unitOfWork.TrainingRepository.GetById(id.Value);
            if (training == null)
            {
                return NotFound();
            }

            ViewData["ClientID"] = new SelectList(_unitOfWork.ClientRepository.DropDownList().OrderBy(c => c.FullName), "ID", "FullName", training.ClientID);
            return View(training);
        }

        // POST: Trainings/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var trainingToUpdate = await _unitOfWork.TrainingRepository.GetById(id.Value);

            if (await TryUpdateModelAsync<Training>(
                trainingToUpdate,
                "",
                t => t.Date, t => t.ClientID));
            {
                try
                {
                    await _unitOfWork.CommitAsync();
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

        // GET: Trainings/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _unitOfWork.TrainingRepository.GetById(id.Value);

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
            var training = await _unitOfWork.TrainingRepository.GetById(id);
            if (training == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _unitOfWork.TrainingRepository.Delete(training);
                await _unitOfWork.CommitAsync();
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
            return _unitOfWork.TrainingRepository.GetById(id) != null;
        }
    }
}
