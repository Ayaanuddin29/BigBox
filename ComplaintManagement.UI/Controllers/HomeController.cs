using Microsoft.AspNetCore.Mvc;

namespace ComplaintManagement.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.User = HttpContext.Session.GetString("UserName") ?? "Guest";
            return View();
        }
    }
}
