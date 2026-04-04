using System.Diagnostics;
using LookClosely_Original.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LookClosely_Original.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult StatusCodeError(int code)
        {
            if (code == 404)
            {
                return View("NotFound"); 
            }

            ViewBag.ErrorCode = code;
            return View("GeneralError");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
