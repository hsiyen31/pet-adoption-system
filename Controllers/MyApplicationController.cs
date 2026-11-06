using Microsoft.AspNetCore.Mvc;
using PetAdoptionSystem.Data;

namespace PetAdoptionSystem.Controllers
{
    public class MyApplicationController : BaseController
    {
        private readonly CmsContext _context;

        public MyApplicationController(CmsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetString("UId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            Guid uid = Guid.Parse(userId);

            var applications = _context.Adoption_Applications
                .Where(x => x.UId_user == uid)
                .OrderByDescending(x => x.ADateTime)
                .ToList();

            return View(applications);
        }
    }
}