using Microsoft.AspNetCore.Mvc;

namespace Examcy.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
