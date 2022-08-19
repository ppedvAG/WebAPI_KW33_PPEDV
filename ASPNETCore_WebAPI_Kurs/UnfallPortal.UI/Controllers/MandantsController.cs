using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UnfallPortal.Shared.Entities;
using UnfallPortal.UI.Data;
using UnfallPortal.UI.Services;

namespace UnfallPortal.UI.Controllers
{
    public class MandantsController : Controller
    {
        private readonly TempContext _context;
        private readonly IMandantService _service;

        public MandantsController(TempContext context, IMandantService service)
        {
            _context = context;
            _service = service;
        }

        // GET: Mandants
        public async Task<IActionResult> Index()
        {
              //return _context.Mandant != null ? 
              //            View(await _context.Mandant.ToListAsync()) :
              //            Problem("Entity set 'TempContext.Mandant'  is null.");

            IList<Mandant> mandants = await _service.GetAll();
            return View(mandants);
        }

        // GET: Mandants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mandant == null)
            {
                return NotFound();
            }

            //var mandant = await _context.Mandant
            //    .FirstOrDefaultAsync(m => m.Id == id);

            Mandant mandant = await _service.GetById(id.Value);

            if (mandant == null)
            {
                return NotFound();
            }

            return View(mandant);
        }

        // GET: Mandants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mandants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName")] Mandant mandant)
        {

            ModelState.Remove("Mandants");
            if (ModelState.IsValid)
            {
                //_context.Add(mandant);
                //await _context.SaveChangesAsync();

                await _service.Insert(mandant);

                return RedirectToAction(nameof(Index));
            }
            return View(mandant);
        }

        // GET: Mandants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mandant == null)
            {
                return NotFound();
            }

            //var mandant = await _context.Mandant.FindAsync(id);
            Mandant mandant = await _service.GetById(id.Value);

           

            if (mandant == null)
            {
                return NotFound();
            }
            return View(mandant);
        }

        // POST: Mandants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName")] Mandant mandant)
        {
            if (id != mandant.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Mandants");

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.Update(id, mandant);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MandantExists(mandant.Id))
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
            return View(mandant);
        }

        // GET: Mandants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mandant == null)
            {
                return NotFound();
            }

            Mandant mandant = await _service.GetById(id.Value);

            //var mandant = await _context.Mandant
            //    .FirstOrDefaultAsync(m => m.Id == id);

            if (mandant == null)
            {
                return NotFound();
            }

            return View(mandant);
        }

        // POST: Mandants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mandant == null)
            {
                return Problem("Entity set 'TempContext.Mandant'  is null.");
            }
            Mandant mandant = await _service.GetById(id);

            if (mandant != null)
            {
                await _service.Delete(mandant.Id);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MandantExists(int id)
        {
          return (_context.Mandant?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
