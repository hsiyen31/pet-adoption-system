using Microsoft.AspNetCore.Mvc;
using PetAdoptionSystem.Data;

namespace PetAdoptionSystem.Controllers
{
    public class PetController : BaseController
    {
        private readonly CmsContext _context;

        public PetController(CmsContext context)
        {
            _context = context;
        }

        public IActionResult Index(string category, int? gender, string ageRange)
        {
            var pets = _context.Pets
                .Where(x => x.pStatus == 0);

            if (!string.IsNullOrEmpty(category))
            {
                pets = pets.Where(x => x.Category == category);
            }

            if (gender != null)
            {
                pets = pets.Where(x => x.Gender == gender);
            }

            if (!string.IsNullOrEmpty(ageRange))
            {
                if (ageRange == "under1")
                {
                    pets = pets.Where(x => x.Age < 1);
                }
                else if (ageRange == "1to5")
                {
                    pets = pets.Where(x => x.Age >= 1 && x.Age <= 5);
                }
                else if (ageRange == "over5")
                {
                    pets = pets.Where(x => x.Age > 5);
                }
            }

            return View(pets.ToList());
        }
        public IActionResult Details(Guid id)
        {
            var pet = _context.Pets
                .FirstOrDefault(x => x.PId == id);

            if (pet == null)
            {
                return NotFound();
            }

            ViewBag.Shelter = _context.Shelters
                .FirstOrDefault(x => x.SId == pet.SId);

            return View(pet);
        }
    }
}