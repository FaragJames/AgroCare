using Microsoft.AspNetCore.Mvc;

namespace AgroCare.Controllers
{
    public class Executive : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
