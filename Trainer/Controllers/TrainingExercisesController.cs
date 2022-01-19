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

        public async Task<IActionResult> Index(string searchString, int page = 1)
        {
            ViewData["CurrentFilter"] = searchString;
            var results = await _unitOfWork.TrainingExerciseRepository
                                        .Include(t => t.Exercise)
                                        .Include(t => t.Training)
                                            .ThenInclude(t => t.Client)
                                        .GetPagedAsync(page, 10);


            if (!String.IsNullOrEmpty(searchString))
            {
                results = results.Where(t => t.Training.Client.FirstName.Contains(searchString)
                                                            || t.Training.Client.LastName.Contains(searchString));
            }

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
            //return await _context.TrainingExercises.GetPagedAsync(page, 10);
            return await _unitOfWork.TrainingExercises.GetPagedAsync(page, 10);
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

            ViewData["ExerciseID"] = new SelectList(_unitOfWork.TrainingExerciseRepository.Exercises, "ExerciseID", "Title");
            ViewData["TrainingID"] = new SelectList(_unitOfWork.TrainingExerciseRepository.Trainings, "TrainingID", "TrainingInfo");

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
            ViewData["ExerciseID"] = new SelectList(_unitOfWork.TrainingExerciseRepository.Exercises, "ExerciseID", "Title", trainingExercise.ExerciseID);
            ViewData["TrainingID"] = new SelectList(_unitOfWork.TrainingExerciseRepository.Trainings, "TrainingID", "TrainingID", trainingExercise.TrainingID);
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
            ViewData["ExerciseID"] = new SelectList(_unitOfWork.TrainingExerciseRepository.Exercises, "ExerciseID", "Title", trainingExercise.ExerciseID);
            ViewData["TrainingID"] = new SelectList(_unitOfWork.TrainingExerciseRepository.Trainings, "TrainingID", "TrainingID", trainingExercise.TrainingID);
            return View(trainingExercise);
        }

        // POST: TrainingExercises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
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
                    await _unitOfWork.CommitAsync();
                    return RedirectToAction(nameof(Index));
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
            ViewData["ExerciseID"] = new SelectList(_unitOfWork.TrainingExerciseRepository.Exercises, "ExerciseID", "Title", trainingExercise.ExerciseID);
            ViewData["TrainingID"] = new SelectList(_unitOfWork.TrainingExerciseRepository.Trainings, "TrainingID", "TrainingID", trainingExercise.TrainingID);
            return View(trainingExercise);
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
