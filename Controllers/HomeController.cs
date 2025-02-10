using HydroSpark.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HydroSpark.Controllers
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

            List<Item> items = new List<Item>
        {
            new Item { Id = 1, Name = "Item 1", Description = "This is item 1", Price = 20.99 },
            new Item { Id = 2, Name = "Item 2", Description = "This is item 2", Price = 30.99 },
            new Item { Id = 3, Name = "Item 3", Description = "This is item 3", Price = 40.99 },
            new Item { Id = 4, Name = "Item 4", Description = "This is item 4", Price = 50.99 },
            new Item { Id = 5, Name = "Item 5", Description = "This is item 5", Price = 60.99 },
            new Item { Id = 1, Name = "Item 1", Description = "This is item 1", Price = 20.99 },
            new Item { Id = 2, Name = "Item 2", Description = "This is item 2", Price = 30.99 },
            new Item { Id = 3, Name = "Item 3", Description = "This is item 3", Price = 40.99 },
            new Item { Id = 4, Name = "Item 4", Description = "This is item 4", Price = 50.99 },
            new Item { Id = 5, Name = "Item 5", Description = "This is item 5", Price = 60.99 },
            // Add more items as necessary
        };
            return View(items);
        }

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
