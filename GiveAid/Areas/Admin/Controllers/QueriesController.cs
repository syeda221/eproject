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
    public class QueriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QueriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Queries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Queries.ToListAsync());
        }

        // GET: Queries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = await _context.Queries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (query == null)
            {
                return NotFound();
            }

            return View(query);
        }

        // GET: Queries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Queries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Message,UserId")] Query query)
        {
            if (ModelState.IsValid)
            {
                _context.Add(query);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(query);
        }

        // GET: Queries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = await _context.Queries.FindAsync(id);
            if (query == null)
            {
                return NotFound();
            }
            return View(query);
        }

        // POST: Queries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Message,UserId")] Query query)
        {
            if (id != query.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(query);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QueryExists(query.Id))
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
            return View(query);
        }

        // GET: Queries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = await _context.Queries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (query == null)
            {
                return NotFound();
            }

            return View(query);
        }

        // POST: Queries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var query = await _context.Queries.FindAsync(id);
            if (query != null)
            {
                _context.Queries.Remove(query);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QueryExists(int id)
        {
            return _context.Queries.Any(e => e.Id == id);
        }
    }
}

