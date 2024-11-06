using Microsoft.AspNetCore.Mvc;

namespace RentaCar.MVC.Controllers
{
    public class CarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            return View();
        }

    }
}
