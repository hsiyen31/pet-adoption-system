using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PetAdoptionSystem.Controllers
{
    public class AdminBaseController : BaseController
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var role = HttpContext.Session.GetInt32("Role");

            if (role != 1)
            {
                context.Result = RedirectToAction("Index", "Pet");
            }
        }
    }
}