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
    public class MandantUnfallDokumentesController : Controller
    {
        private readonly TempContext _context;

        public MandantUnfallDokumentesController(TempContext context)
        {
            _context = context;
        }

        // GET: MandantUnfallDokumentes
        public async Task<IActionResult> Index()
        {
            var tempContext = _context.MandantUnfallDokumente.Include(m => m.MandantUnfall);
            return View(await tempContext.ToListAsync());
        }

        // GET: MandantUnfallDokumentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MandantUnfallDokumente == null)
            {
                return NotFound();
            }

            var mandantUnfallDokumente = await _context.MandantUnfallDokumente
                .Include(m => m.MandantUnfall)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mandantUnfallDokumente == null)
            {
                return NotFound();
            }

            return View(mandantUnfallDokumente);
        }

        // GET: MandantUnfallDokumentes/Create
        public IActionResult Create()
        {
            ViewData["MandantUnfallId"] = new SelectList(_context.MandantUnfall, "Id", "UnfallName");
            return View();
        }

        // POST: MandantUnfallDokumentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MandantUnfallId,Name,Dateinamen")] MandantUnfallDokumente mandantUnfallDokumente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mandantUnfallDokumente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MandantUnfallId"] = new SelectList(_context.MandantUnfall, "Id", "UnfallName", mandantUnfallDokumente.MandantUnfallId);
            return View(mandantUnfallDokumente);
        }

        // GET: MandantUnfallDokumentes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MandantUnfallDokumente == null)
            {
                return NotFound();
            }

            var mandantUnfallDokumente = await _context.MandantUnfallDokumente.FindAsync(id);
            if (mandantUnfallDokumente == null)
            {
                return NotFound();
            }
            ViewData["MandantUnfallId"] = new SelectList(_context.MandantUnfall, "Id", "UnfallName", mandantUnfallDokumente.MandantUnfallId);
            return View(mandantUnfallDokumente);
        }

        // POST: MandantUnfallDokumentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MandantUnfallId,Name,Dateinamen")] MandantUnfallDokumente mandantUnfallDokumente)
        {
            if (id != mandantUnfallDokumente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mandantUnfallDokumente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MandantUnfallDokumenteExists(mandantUnfallDokumente.Id))
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
            ViewData["MandantUnfallId"] = new SelectList(_context.MandantUnfall, "Id", "UnfallName", mandantUnfallDokumente.MandantUnfallId);
            return View(mandantUnfallDokumente);
        }

        // GET: MandantUnfallDokumentes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MandantUnfallDokumente == null)
            {
                return NotFound();
            }

            var mandantUnfallDokumente = await _context.MandantUnfallDokumente
                .Include(m => m.MandantUnfall)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mandantUnfallDokumente == null)
            {
                return NotFound();
            }

            return View(mandantUnfallDokumente);
        }

        // POST: MandantUnfallDokumentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MandantUnfallDokumente == null)
            {
                return Problem("Entity set 'TempContext.MandantUnfallDokumente'  is null.");
            }
            var mandantUnfallDokumente = await _context.MandantUnfallDokumente.FindAsync(id);
            if (mandantUnfallDokumente != null)
            {
                _context.MandantUnfallDokumente.Remove(mandantUnfallDokumente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MandantUnfallDokumenteExists(int id)
        {
          return (_context.MandantUnfallDokumente?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
