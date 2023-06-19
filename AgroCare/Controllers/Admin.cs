using Microsoft.AspNetCore.Mvc;
using Services;
using Models;
using Microsoft.AspNetCore.Authorization;

namespace AgroCare.Controllers
{
    [Authorize(Roles = "Admin")]
    public class Admin : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
