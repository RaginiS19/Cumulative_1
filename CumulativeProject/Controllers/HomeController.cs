using System.Diagnostics;
using CumulativeProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace CumulativeProject.Controllers
{
    /// <summary>
    /// HomeController handles requests related to the home page and general site navigation.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        /// <summary>
        /// Initializes a new instance of the HomeController with a logger.
        /// </summary>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
         /// <summary>
        /// Serves the homepage of the application.
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Serves the Privacy Policy page.
        /// </summary>
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
