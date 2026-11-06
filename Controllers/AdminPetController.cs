using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetAdoptionSystem.Data;
using PetAdoptionSystem.Models;

namespace PetAdoptionSystem.Controllers
{
    public class AdminPetController : AdminBaseController
    {
        private readonly CmsContext _context;
        private readonly IWebHostEnvironment _env;

        public AdminPetController(CmsContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // 寵物列表
        public IActionResult Index(int? status)
        {
            var pets = _context.Pets.AsQueryable();

            // 狀態篩選
            if (status != null)
            {
                pets = pets.Where(x => x.pStatus == status);
            }

            ViewBag.Status = status;

            return View(pets.ToList());
        }

        // 新增頁面
        public IActionResult Create()
        {
            ViewBag.ShelterList = new SelectList(
                _context.Shelters.ToList(),
                "SId",
                "OrgName");

            return View();
        }

        // 新增功能
        [HttpPost]
        public IActionResult Create(Pet pet, IFormFile photoFile)
        {
            pet.PId = Guid.NewGuid();

            // 預設待領養
            pet.pStatus = 0;

            // 處理圖片上傳
            if (photoFile != null)
            {
                string fileName = Guid.NewGuid().ToString()
                                  + Path.GetExtension(photoFile.FileName);

                string uploadPath = Path.Combine(
                    _env.WebRootPath,
                    "images",
                    fileName);

                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    photoFile.CopyTo(stream);
                }

                pet.Photo = fileName;
            }

            _context.Pets.Add(pet);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // 修改頁面
        public IActionResult Edit(Guid id)
        {
            var pet = _context.Pets.FirstOrDefault(x => x.PId == id);

            if (pet == null)
            {
                return NotFound();
            }

            ViewBag.ShelterList = new SelectList(
                _context.Shelters.ToList(),
                "SId",
                "OrgName",
                pet.SId);

            return View(pet);
        }

        [HttpPost]
        public IActionResult Edit(Pet pet, IFormFile photoFile)
        {
            var oldPet = _context.Pets.FirstOrDefault(x => x.PId == pet.PId);

            if (oldPet == null)
            {
                return NotFound();
            }

            oldPet.PetName = pet.PetName;
            oldPet.Category = pet.Category;
            oldPet.Gender = pet.Gender;
            oldPet.Age = pet.Age;
            oldPet.Size = pet.Size;
            oldPet.HealthRecord = pet.HealthRecord;
            oldPet.pStatus = pet.pStatus;
            oldPet.SId = pet.SId;

            if (photoFile != null)
            {
                string fileName = Guid.NewGuid().ToString()
                                  + Path.GetExtension(photoFile.FileName);

                string uploadPath = Path.Combine(
                    _env.WebRootPath,
                    "images",
                    fileName);

                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    photoFile.CopyTo(stream);
                }

                oldPet.Photo = fileName;
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // 直接刪除
        public IActionResult Delete(Guid id)
        {
            var pet = _context.Pets.FirstOrDefault(x => x.PId == id);

            if (pet != null)
            {
                _context.Pets.Remove(pet);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}