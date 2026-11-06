using Microsoft.AspNetCore.Mvc;
using PetAdoptionSystem.Data;
using PetAdoptionSystem.Models;

namespace PetAdoptionSystem.Controllers
{
    public class AdoptionController : BaseController
    {
        private readonly CmsContext _context;

        public AdoptionController(CmsContext context)
        {
            _context = context;
        }

        // 申請單頁面
        public IActionResult Create(Guid id)
        {
            var userId = HttpContext.Session.GetString("UId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var pet = _context.Pets.FirstOrDefault(x => x.PId == id);

            if (pet == null)
            {
                return NotFound();
            }

            ViewBag.Pet = pet;

            return View();
        }

        // 送出申請單
        [HttpPost]
        public IActionResult Create(Adoption_Application application)
        {
            var userId = HttpContext.Session.GetString("UId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            Guid uid = Guid.Parse(userId);

            var user = _context.Users.FirstOrDefault(x => x.UId == uid);

            if (user == null)
            {
                return Content("找不到目前登入的使用者，請重新登入後再申請。Session UId = " + userId);
            }

            application.uNo = Guid.NewGuid();
            application.ADateTime = DateTime.Now;
            application.Astatus = 0; // 0 = 審核中
            application.UId_user = uid;
            application.UId_manager = null;

            var pet = _context.Pets.FirstOrDefault(x => x.PId == application.PId);

            if (pet == null)
            {
                return NotFound();
            }

            pet.pStatus = 1; // 1 = 申請中

            _context.Adoption_Applications.Add(application);
            _context.SaveChanges();

            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}