using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UnfallPortal.Shared.Entities;
using UnfallPortal.UI.Data;

namespace UnfallPortal.UI.Controllers
{
    public class ErsteHilfeKursController : Controller
    {
        private readonly TempContext _context;

        public ErsteHilfeKursController(TempContext context)
        {
            _context = context;
        }

        // GET: ErsteHilfeKurs
        public async Task<IActionResult> Index()
        {
              return _context.ErsteHilfeKurs != null ? 
                          View(await _context.ErsteHilfeKurs.ToListAsync()) :
                          Problem("Entity set 'TempContext.ErsteHilfeKurs'  is null.");
        }

        // GET: ErsteHilfeKurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ErsteHilfeKurs == null)
            {
                return NotFound();
            }

            var ersteHilfeKurs = await _context.ErsteHilfeKurs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ersteHilfeKurs == null)
            {
                return NotFound();
            }

            return View(ersteHilfeKurs);
        }

        // GET: ErsteHilfeKurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ErsteHilfeKurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Ort,Datum")] ErsteHilfeKurs ersteHilfeKurs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ersteHilfeKurs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ersteHilfeKurs);
        }

        // GET: ErsteHilfeKurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ErsteHilfeKurs == null)
            {
                return NotFound();
            }

            var ersteHilfeKurs = await _context.ErsteHilfeKurs.FindAsync(id);
            if (ersteHilfeKurs == null)
            {
                return NotFound();
            }
            return View(ersteHilfeKurs);
        }

        // POST: ErsteHilfeKurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Ort,Datum")] ErsteHilfeKurs ersteHilfeKurs)
        {
            if (id != ersteHilfeKurs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ersteHilfeKurs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ErsteHilfeKursExists(ersteHilfeKurs.Id))
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
            return View(ersteHilfeKurs);
        }

        // GET: ErsteHilfeKurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ErsteHilfeKurs == null)
            {
                return NotFound();
            }

            var ersteHilfeKurs = await _context.ErsteHilfeKurs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ersteHilfeKurs == null)
            {
                return NotFound();
            }

            return View(ersteHilfeKurs);
        }

        // POST: ErsteHilfeKurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ErsteHilfeKurs == null)
            {
                return Problem("Entity set 'TempContext.ErsteHilfeKurs'  is null.");
            }
            var ersteHilfeKurs = await _context.ErsteHilfeKurs.FindAsync(id);
            if (ersteHilfeKurs != null)
            {
                _context.ErsteHilfeKurs.Remove(ersteHilfeKurs);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ErsteHilfeKursExists(int id)
        {
          return (_context.ErsteHilfeKurs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
