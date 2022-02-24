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

namespace Trainer.Controllers
{
    public class TrainingExercisesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrainingExercisesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int? id, string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            IEnumerable<TrainingExercise> results = await _unitOfWork.TrainingExerciseRepository.List(searchString);

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

            var trainingExercise = await _unitOfWork.TrainingExerciseRepository
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

            ViewData["ExerciseID"] = new SelectList(_unitOfWork.ExerciseRepository.DropDownList().OrderBy(e => e.Title), "ID", "Title");
            ViewData["TrainingID"] = new SelectList(_unitOfWork.TrainingRepository.DropDownList(), "ID", "ID");

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
                await _unitOfWork.TrainingExerciseRepository.Save(trainingExercise);
                await _unitOfWork.CommitAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExerciseID"] = new SelectList(_unitOfWork.TrainingExerciseRepository.Exercises, "ID", "Title", trainingExercise.ExerciseID);
            ViewData["TrainingID"] = new SelectList(_unitOfWork.TrainingExerciseRepository.Trainings, "ID", "ID", trainingExercise.TrainingID);
            return View(trainingExercise);
        }

        // GET: TrainingExercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingExercise = await _unitOfWork.TrainingExerciseRepository.GetById(id.Value);
            if (trainingExercise == null)
            {
                return NotFound();
            }
            ViewData["ExerciseID"] = new SelectList(_unitOfWork.ExerciseRepository.DropDownList().OrderBy(e => e.Title), "ID", "Title");
            ViewData["TrainingID"] = new SelectList(_unitOfWork.TrainingRepository.DropDownList(), "ID", "ID");
            ViewData["TrainingID"] = new SelectList(_unitOfWork.TrainingRepository.DropDownList(), "ID", "ID");
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

            var trainingExerciseToUpdate = await _unitOfWork.TrainingExerciseRepository.GetById(id.Value);

            if (await TryUpdateModelAsync<TrainingExercise>(
                trainingExerciseToUpdate,
                "",
                t => t.TrainingID, t => t.ExerciseID, t => t.Rounds, t => t.Repetitions, t => t.MaxWeight, t => t.Comments));
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

        // GET: TrainingExercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingExercise = await _unitOfWork.TrainingExerciseRepository.GetById(id.Value);

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
            var trainingExercise = await _unitOfWork.TrainingExerciseRepository.GetById(id);

            _unitOfWork.TrainingExerciseRepository.Delete(trainingExercise);
            await _unitOfWork.CommitAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingExerciseExists(int id)
        {
            return _unitOfWork.TrainingExerciseRepository.GetById(id) != null;
        }
    }
}
