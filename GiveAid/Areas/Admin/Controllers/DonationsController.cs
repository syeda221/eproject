using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiveAid.Data;
using GiveAid.Models;

namespace GiveAid.Areas_Admin_Controllers
{
    [Microsoft.AspNetCore.Mvc.Area("Admin")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class DonationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Admin/Donations
        public async Task<IActionResult> Index()
        {
            var donations = await _context.Donations
                .Include(d => d.Cause)
                .OrderByDescending(d => d.Id)
                .ToListAsync();

            ViewBag.TotalFunds = donations.Sum(d => d.Amount);
            ViewBag.TotalCount = donations.Count;

            return View(donations);
        }

        // GET: /Admin/Donations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donations
                .Include(d => d.Cause)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (donation == null)
            {
                return NotFound();
            }

            return View(donation);
        }

        // POST: /Admin/Donations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donation = await _context.Donations.FindAsync(id);
            if (donation != null)
            {
                _context.Donations.Remove(donation);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
