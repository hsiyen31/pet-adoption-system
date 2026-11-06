using Microsoft.AspNetCore.Mvc;
using PetAdoptionSystem.Data;

namespace PetAdoptionSystem.Controllers
{
    public class AdminApplicationController : AdminBaseController
    {
        private readonly CmsContext _context;

        public AdminApplicationController(CmsContext context)
        {
            _context = context;
        }

        // 申請單列表
        public IActionResult Index()
        {
            var applications = _context.Adoption_Applications.ToList();

            return View(applications);
        }

        // 通過申請
        public IActionResult Approve(Guid id)
        {
            var application =
                _context.Adoption_Applications
                .FirstOrDefault(x => x.uNo == id);

            if (application == null)
            {
                return NotFound();
            }

            // 申請單狀態 → 已通過
            application.Astatus = 1;

            // 紀錄審核管理員
            var managerId =
                HttpContext.Session.GetString("UId");

            if (!string.IsNullOrEmpty(managerId))
            {
                application.UId_manager =
                    Guid.Parse(managerId);
            }

            // 寵物狀態 → 已領養
            var pet =
                _context.Pets
                .FirstOrDefault(x => x.PId == application.PId);

            if (pet != null)
            {
                pet.pStatus = 2;
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // 拒絕申請
        public IActionResult Reject(Guid id)
        {
            var application =
                _context.Adoption_Applications
                .FirstOrDefault(x => x.uNo == id);

            if (application == null)
            {
                return NotFound();
            }

            // 申請單狀態 → 已拒絕
            application.Astatus = 2;

            // 紀錄審核管理員
            var managerId =
                HttpContext.Session.GetString("UId");

            if (!string.IsNullOrEmpty(managerId))
            {
                application.UId_manager =
                    Guid.Parse(managerId);
            }

            // 寵物重新回到待領養
            var pet =
                _context.Pets
                .FirstOrDefault(x => x.PId == application.PId);

            if (pet != null)
            {
                pet.pStatus = 0;
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}