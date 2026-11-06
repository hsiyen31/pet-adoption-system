using Microsoft.AspNetCore.Mvc;

namespace PetAdoptionSystem.Controllers
{
    public class AdminController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}