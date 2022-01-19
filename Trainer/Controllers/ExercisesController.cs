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

namespace Trainer.Controllers
{
    public class ExercisesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExercisesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Exercises
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["MuscleSortParm"] = sortOrder == "Muscle" ? "muscle_desc" : "Muscle";
            ViewData["CurrentFilter"] = searchString;
            IEnumerable<Exercise> exercises = await _unitOfWork.ExerciseRepository.List();

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

            var exercise = await _unitOfWork.ExerciseRepository.GetById(id.Value);

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
                await _unitOfWork.ExerciseRepository.Save(exercise);
                await _unitOfWork.CommitAsync();
                return RedirectToAction(nameof(Index));
            }

            var statuses = from MuscleGroup s in Enum.GetValues(typeof(MuscleGroup))
                           select new {Name = s.ToString() };
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

            var exercise = await _unitOfWork.ExerciseRepository.GetById(id.Value);
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
        public async Task<IActionResult> Edit(int id, [Bind("ExerciseID,Title,MuscleGroup")] Exercise exercise)
        {
            if (id != exercise.ID)
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
                    if (!ExerciseExists(exercise.ID))
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
            return View(exercise);
        }

        // GET: Exercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _unitOfWork.ExerciseRepository.GetById(id.Value);

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
            var exercise = await _unitOfWork.ExerciseRepository.GetById(id);
            _unitOfWork.ExerciseRepository.Delete(exercise);
            await _unitOfWork.CommitAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseExists(int id)
        {
            return _unitOfWork.ExerciseRepository.GetById(id) != null;
        }
    }
}
