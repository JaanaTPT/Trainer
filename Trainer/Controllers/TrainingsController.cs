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
            ViewData["ClientID"] = new SelectList(_unitOfWork.TrainingRepository.Clients, "ID", "FullName");
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
            ViewData["ClientID"] = new SelectList(_unitOfWork.TrainingRepository.Clients, "ID", "FirstName", training.ClientID);
            return View(training);
        }

        // POST: Trainings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrainingID,Date,ClientID")] Training training)
        {
            if (id != training.ID)
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
                    if (!TrainingExists(training.ID))
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
            ViewData["ClientID"] = new SelectList(_unitOfWork.TrainingRepository.Clients, "ID", "FirstName", training.ClientID);
            return View(training);
        }

        // GET: Trainings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var training = await _context.Trainings
            //    .Include(t => t.Client)
            //    .FirstOrDefaultAsync(m => m.ID == id);

            var training = _unitOfWork.TrainingRepository.GetById(id.Value);

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
            var training = await _unitOfWork.TrainingRepository.GetById(id);
            _unitOfWork.TrainingRepository.Delete(training);
            await _unitOfWork.CommitAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingExists(int id)
        {
            return _unitOfWork.TrainingRepository.GetById(id) != null;
        }
    }
}
