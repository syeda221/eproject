using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiveAid.Data;
using GiveAid.Models;

namespace GiveAid.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /
        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel
            {
                Causes = await _context.Causes.ToListAsync(),
                Programmes = await _context.Programmes.Include(p => p.Ngo).ToListAsync(),
                NGOs = await _context.NGOs.ToListAsync(),
                Galleries = await _context.Galleries.ToListAsync()
            };

            return View(model);
        }

        // GET: /Home/About
        public IActionResult About()
        {
            return View();
        }

        // GET: /Home/Partners
        public IActionResult Partners()
        {
            return View();
        }

        // GET: /Home/HelpCentre
        public IActionResult HelpCentre()
        {
            return View(new QueryFormViewModel());
        }

        // POST: /Home/SubmitQuery
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitQuery(QueryFormViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Message))
            {
                TempData["ErrorMessage"] = "Please write your query/message before submitting.";
                return RedirectToAction(nameof(HelpCentre));
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? (string.IsNullOrWhiteSpace(model.Email) ? "Guest" : model.Email);

            var query = new Query
            {
                Message = $"[From: {model.FullName} | Email: {model.Email}] {model.Message}",
                UserId = userId
            };

            _context.Queries.Add(query);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Thank you! Your query has been submitted to the Give-AID team. We will get back to you shortly.";
            return RedirectToAction(nameof(HelpCentre));
        }

        // POST: /Home/InviteFriend
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InviteFriend(string friendEmail)
        {
            if (!string.IsNullOrWhiteSpace(friendEmail))
            {
                TempData["SuccessMessage"] = $"An invitation email to join Give-AID has been sent to {friendEmail}!";
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: /Home/JoinProgramme
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult JoinProgramme(int programmeId, string volunteerName, string volunteerEmail)
        {
            if (!string.IsNullOrWhiteSpace(volunteerEmail))
            {
                TempData["SuccessMessage"] = $"Thank you {volunteerName}! Your interest to participate has been recorded. Our NGO coordinator will contact you soon.";
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
