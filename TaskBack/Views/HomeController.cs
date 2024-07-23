using Microsoft.AspNetCore.Mvc;

namespace TaskBack.Views
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Redirect to Departments/Index
            return RedirectToAction("Index", "Departments");
        }
    }
}
