using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GiveAid.Data;
using GiveAid.Models;

namespace GiveAid.Areas_Admin_Controllers
{
    [Microsoft.AspNetCore.Mvc.Area("Admin")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class NGOsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NGOsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NGOs
        public async Task<IActionResult> Index()
        {
            return View(await _context.NGOs.ToListAsync());
        }

        // GET: NGOs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ngo = await _context.NGOs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ngo == null)
            {
                return NotFound();
            }

            return View(ngo);
        }

        // GET: NGOs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NGOs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NgoName,Description")] Ngo ngo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ngo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ngo);
        }

        // GET: NGOs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ngo = await _context.NGOs.FindAsync(id);
            if (ngo == null)
            {
                return NotFound();
            }
            return View(ngo);
        }

        // POST: NGOs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NgoName,Description")] Ngo ngo)
        {
            if (id != ngo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ngo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NgoExists(ngo.Id))
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
            return View(ngo);
        }

        // GET: NGOs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ngo = await _context.NGOs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ngo == null)
            {
                return NotFound();
            }

            return View(ngo);
        }

        // POST: NGOs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ngo = await _context.NGOs.FindAsync(id);
            if (ngo != null)
            {
                _context.NGOs.Remove(ngo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NgoExists(int id)
        {
            return _context.NGOs.Any(e => e.Id == id);
        }
    }
}

