using System;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiveAid.Data;
using GiveAid.Models;

namespace GiveAid.Controllers
{
    public class DonateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonateController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Donate
        public async Task<IActionResult> Index(int? causeId, decimal? amount)
        {
            var causes = await _context.Causes.ToListAsync();
            var model = new DonationFormViewModel
            {
                AvailableCauses = causes,
                CauseId = causeId ?? (causes.FirstOrDefault()?.Id ?? 0),
                Amount = amount.HasValue && amount.Value > 0 ? amount.Value : 1000 // Pre-filled or default suggestion in PKR
            };

            return View(model);
        }

        // POST: /Donate/Process
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Process(DonationFormViewModel model)
        {
            var causes = await _context.Causes.ToListAsync();
            model.AvailableCauses = causes;

            // Server-side validations
            if (model.Amount <= 0)
            {
                ModelState.AddModelError("Amount", "Please enter a valid donation amount greater than PKR 0.");
            }

            if (string.IsNullOrWhiteSpace(model.CardHolderName))
            {
                ModelState.AddModelError("CardHolderName", "Cardholder name is required.");
            }

            var cleanCardNumber = (model.CardNumber ?? "").Replace(" ", "").Replace("-", "");
            if (!Regex.IsMatch(cleanCardNumber, @"^\d{16}$"))
            {
                ModelState.AddModelError("CardNumber", "Card number must be exactly 16 digits.");
            }

            if (!Regex.IsMatch(model.ExpiryDate ?? "", @"^(0[1-9]|1[0-2])\/?([0-9]{2})$"))
            {
                ModelState.AddModelError("ExpiryDate", "Expiry date must be in MM/YY format.");
            }

            if (!Regex.IsMatch(model.CVV ?? "", @"^\d{3}$"))
            {
                ModelState.AddModelError("CVV", "CVV must be 3 digits.");
            }

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            // Get logged in user or mark as Anonymous
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Anonymous";

            var donation = new Donation
            {
                Amount = model.Amount,
                CauseId = model.CauseId,
                UserId = userId
            };

            _context.Donations.Add(donation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Success), new { id = donation.Id });
        }

        // GET: /Donate/Success/5
        public async Task<IActionResult> Success(int id)
        {
            var donation = await _context.Donations
                .Include(d => d.Cause)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (donation == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(donation);
        }
    }
}
