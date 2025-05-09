using Microsoft.AspNetCore.Mvc;

namespace TeamInsights.Controllers
{
    public class SplashController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Aboutus()
        {
            return View();
        }
    }
}
