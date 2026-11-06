using Microsoft.AspNetCore.Mvc;
using PetAdoptionSystem.Data;
using PetAdoptionSystem.Models;

namespace PetAdoptionSystem.Controllers
{
    public class ShelterController : AdminBaseController
    {
        private readonly CmsContext _context;

        public ShelterController(CmsContext context)
        {
            _context = context;
        }

        // 收容所列表
        public IActionResult Index()
        {
            var shelters = _context.Shelters.ToList();

            return View(shelters);
        }

        // 新增頁面
        public IActionResult Create()
        {
            return View();
        }

        // 新增功能
        [HttpPost]
        public IActionResult Create(Shelter shelter)
        {
            shelter.SId = Guid.NewGuid();

            _context.Shelters.Add(shelter);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // 修改頁面
        public IActionResult Edit(Guid id)
        {
            var shelter = _context.Shelters
                .FirstOrDefault(x => x.SId == id);

            if (shelter == null)
            {
                return NotFound();
            }

            return View(shelter);
        }

        // 修改功能
        [HttpPost]
        public IActionResult Edit(Shelter shelter)
        {
            _context.Shelters.Update(shelter);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // 刪除功能
        public IActionResult Delete(Guid id)
        {
            var shelter = _context.Shelters
                .FirstOrDefault(x => x.SId == id);

            if (shelter != null)
            {
                _context.Shelters.Remove(shelter);

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}