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
    public class MandantKursController : Controller
    {
        private readonly TempContext _context;

        public MandantKursController(TempContext context)
        {
            _context = context;
        }

        // GET: MandantKurs
        public async Task<IActionResult> Index()
        {
            var tempContext = _context.MandantKurs.Include(m => m.Mandant);
            return View(await tempContext.ToListAsync());
        }

        // GET: MandantKurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MandantKurs == null)
            {
                return NotFound();
            }

            var mandantKurs = await _context.MandantKurs
                .Include(m => m.Mandant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mandantKurs == null)
            {
                return NotFound();
            }

            return View(mandantKurs);
        }

        // GET: MandantKurs/Create
        public IActionResult Create()
        {
            ViewData["MandantId"] = new SelectList(_context.Mandant, "Id", "FirstName");
            return View();
        }

        // POST: MandantKurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MandantId,KursId")] MandantKurs mandantKurs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mandantKurs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MandantId"] = new SelectList(_context.Mandant, "Id", "FirstName", mandantKurs.MandantId);
            return View(mandantKurs);
        }

        // GET: MandantKurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MandantKurs == null)
            {
                return NotFound();
            }

            var mandantKurs = await _context.MandantKurs.FindAsync(id);
            if (mandantKurs == null)
            {
                return NotFound();
            }
            ViewData["MandantId"] = new SelectList(_context.Mandant, "Id", "FirstName", mandantKurs.MandantId);
            return View(mandantKurs);
        }

        // POST: MandantKurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MandantId,KursId")] MandantKurs mandantKurs)
        {
            if (id != mandantKurs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mandantKurs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MandantKursExists(mandantKurs.Id))
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
            ViewData["MandantId"] = new SelectList(_context.Mandant, "Id", "FirstName", mandantKurs.MandantId);
            return View(mandantKurs);
        }

        // GET: MandantKurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MandantKurs == null)
            {
                return NotFound();
            }

            var mandantKurs = await _context.MandantKurs
                .Include(m => m.Mandant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mandantKurs == null)
            {
                return NotFound();
            }

            return View(mandantKurs);
        }

        // POST: MandantKurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MandantKurs == null)
            {
                return Problem("Entity set 'TempContext.MandantKurs'  is null.");
            }
            var mandantKurs = await _context.MandantKurs.FindAsync(id);
            if (mandantKurs != null)
            {
                _context.MandantKurs.Remove(mandantKurs);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MandantKursExists(int id)
        {
          return (_context.MandantKurs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
