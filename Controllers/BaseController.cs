using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PetAdoptionSystem.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = HttpContext.Session.GetString("UId");

            if (string.IsNullOrEmpty(userId))
            {
                context.Result = RedirectToAction("Login", "Account");
            }

            base.OnActionExecuting(context);
        }
    }
}