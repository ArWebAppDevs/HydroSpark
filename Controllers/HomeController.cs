using HydroSpark.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HydroSpark.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public List<Products> GetAllProducts()
    {
        return _context.Products.ToList();  // Fetch all products
    }

    // Action method to show all products
    public IActionResult Products()
    {
        var products = GetAllProducts(); 
        return View(products); 
    }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        // }

        [HttpGet("signup")]
        public IActionResult register()
        {

            return View();

        }

        [HttpPost("signup")]
        public IActionResult register(IFormCollection form)
        {
            string email = form["Email"];
            string password = form["Password"];
            string cnfPassword = form["cnfPassword"];
            bool userExists = _context.Users
                             .Any(u => u.Email == email);
            Console.WriteLine(userExists);
            if (userExists == true)
            {
                TempData["msg"] = "User Exists";
                return View();
            }
            if (form["cnfPassword"].Equals(form["Password"]))
            {
                User user = new User { Email = email, Password = password };
                _context.Users.Add(user);
                _context.SaveChanges();
                TempData["success"] = "Sucessfully Register";
                return RedirectToAction("Index");
            }

            if (!cnfPassword.Equals(password))
            {
                TempData["msg"] = "Password and confirm password not matching";
            }

            return View();

        }
        [HttpGet("signin")]
        public IActionResult signin()
        {

            return View();

        }

        [HttpPost("signin")]
        public IActionResult signin(IFormCollection form)
        {
            string email = form["Email"];
            string password = form["Password"];
            bool userExists = _context.Users
                             .Any(u => u.Email == email && u.Password == password);
            if (userExists == false)
            {
                TempData["msg"] = "Username or Password Incorrect";
            }
            else
            {
                HttpContext.Session.SetString("user", email);
                TempData["user"]= email;
                return RedirectToAction("");
            }

            return View();
        }


        [HttpGet("profile")]
        public IActionResult Profile(IFormCollection form)
        {
            string user = HttpContext.Session.GetString("user");
            if (user == null)
            {
                return RedirectToAction("signin");
            }
            var currentUser = _context.Users.Where(u => u.Email == user);
            TempData["currUser"] = currentUser;

            return View();

        }

        

    }
}
