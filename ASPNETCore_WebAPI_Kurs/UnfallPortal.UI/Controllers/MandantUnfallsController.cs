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
    public class MandantUnfallsController : Controller
    {
        private readonly TempContext _context;

        public MandantUnfallsController(TempContext context)
        {
            _context = context;
        }

        // GET: MandantUnfalls
        public async Task<IActionResult> Index()
        {
            var tempContext = _context.MandantUnfall.Include(m => m.MandantRef);
            return View(await tempContext.ToListAsync());
        }

        // GET: MandantUnfalls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MandantUnfall == null)
            {
                return NotFound();
            }

            var mandantUnfall = await _context.MandantUnfall
                .Include(m => m.MandantRef)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mandantUnfall == null)
            {
                return NotFound();
            }

            return View(mandantUnfall);
        }

        // GET: MandantUnfalls/Create
        public IActionResult Create()
        {
            ViewData["MandantId"] = new SelectList(_context.Mandant, "Id", "FirstName");
            return View();
        }

        // POST: MandantUnfalls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MandantId,UnfallName")] MandantUnfall mandantUnfall)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mandantUnfall);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MandantId"] = new SelectList(_context.Mandant, "Id", "FirstName", mandantUnfall.MandantId);
            return View(mandantUnfall);
        }

        // GET: MandantUnfalls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MandantUnfall == null)
            {
                return NotFound();
            }

            var mandantUnfall = await _context.MandantUnfall.FindAsync(id);
            if (mandantUnfall == null)
            {
                return NotFound();
            }
            ViewData["MandantId"] = new SelectList(_context.Mandant, "Id", "FirstName", mandantUnfall.MandantId);
            return View(mandantUnfall);
        }

        // POST: MandantUnfalls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MandantId,UnfallName")] MandantUnfall mandantUnfall)
        {
            if (id != mandantUnfall.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mandantUnfall);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MandantUnfallExists(mandantUnfall.Id))
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
            ViewData["MandantId"] = new SelectList(_context.Mandant, "Id", "FirstName", mandantUnfall.MandantId);
            return View(mandantUnfall);
        }

        // GET: MandantUnfalls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MandantUnfall == null)
            {
                return NotFound();
            }

            var mandantUnfall = await _context.MandantUnfall
                .Include(m => m.MandantRef)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mandantUnfall == null)
            {
                return NotFound();
            }

            return View(mandantUnfall);
        }

        // POST: MandantUnfalls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MandantUnfall == null)
            {
                return Problem("Entity set 'TempContext.MandantUnfall'  is null.");
            }
            var mandantUnfall = await _context.MandantUnfall.FindAsync(id);
            if (mandantUnfall != null)
            {
                _context.MandantUnfall.Remove(mandantUnfall);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MandantUnfallExists(int id)
        {
          return (_context.MandantUnfall?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
