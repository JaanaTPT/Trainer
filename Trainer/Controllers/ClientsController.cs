using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Core.IConfiguration;
using Trainer.Models;

namespace Trainer.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Clients
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "lastName_desc" : "";
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "firstName_desc" : "";
            ViewData["CurrentFilter"] = searchString;
            IEnumerable<Client> clients = await _unitOfWork.ClientRepository.List();
                       
            if (!String.IsNullOrEmpty(searchString))
            {
                clients = clients.Where(s => s.LastName.ToLower().Contains(searchString.ToLower())
                                       || s.FirstName.ToLower().Contains(searchString.ToLower()));
            }
            switch (sortOrder)
            {
                case "lastName_desc":
                    clients = clients.OrderByDescending(s => s.LastName);
                    break;
                case "firstName_desc":
                    clients = clients.OrderByDescending(s => s.FirstName);
                    break;
                default:
                    clients = clients.OrderBy(s => s.FirstName);
                    break;
            }
            return View(clients.ToList());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _unitOfWork.ClientRepository
               .GetById(id.Value);

            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,DateOfBirth,Gender,StartWeight,CurrentWeight,Height,AdditionalInfo")] Client client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _unitOfWork.ClientRepository.Save(client);
                    await _unitOfWork.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _unitOfWork.ClientRepository.GetById(id.Value);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var clientToUpdate = await _unitOfWork.ClientRepository.GetById(id.Value);
            if (await TryUpdateModelAsync<Client>(
                clientToUpdate,
                "",
                c => c.FirstName, c => c.LastName, c => c.DateOfBirth, c => c.Gender, c => c.StartWeight, c => c.CurrentWeight, c => c.Height, c => c.AdditionalInfo))
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
            return View(clientToUpdate);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _unitOfWork.ClientRepository.GetById(id.Value);

            if (client == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _unitOfWork.ClientRepository.GetById(id);
            if (client == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _unitOfWork.ClientRepository.Delete(client);
                await _unitOfWork.CommitAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool ClientExists(int id)
        {
            return _unitOfWork.ClientRepository.GetById(id) != null;
        }
    }
}
