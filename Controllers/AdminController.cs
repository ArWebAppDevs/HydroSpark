using Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HydroSpark.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace HydroSpark.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {

        private AppDbContext _context;

        private readonly ILogger<HomeController> _logger;

        public AdminController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Route("")]
        public IActionResult AdminPanel()
        {

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
            bool userExists = _context.Employee
                             .Any(u => u.Email == email && u.Password == password);
            if (userExists == false)
            {
                TempData["msg"] = "Username or Password Incorrect";
            }
            else
            {
                HttpContext.Session.SetString("user", email);
                return RedirectToAction("/admin/");
            }

            return View();

        }

        [HttpGet("addEmployee")]
        public IActionResult AddEmployee()
        {

            String user = HttpContext.Session.GetString("user");
            if (user == null)
            {
                return Redirect("/admin/signin");
            }
            var employee = _context.Employee
                .Include(e => e.Roles)
                .FirstOrDefault(e => e.Email == user);

            bool chq = false;
            foreach (Role r in employee.Roles)
            {
                if (r.RoleName.ToLower().Equals("owner"))
                {
                    chq = true;
                    break;
                }
                Console.WriteLine(r.RoleName);
            }
            if (chq == false)
            {
                return RedirectToAction("/employee/error");
            }
            else
            {
                return View(); ;
            }

        }



        [HttpPost("/error")]
        public String error()
        {
            return "User is not authorize to do operations";
        }

        [HttpPost("addEmployee")]
        public IActionResult AddEmployee(IFormCollection form)
        {
            String firstName = form["FirstName"];
            String lastName = form["LastName"];
            String email = form["Email"];
            string mobileNumber = form["Mobile"];
            string password = firstName + lastName + "@" + 123;
            List<string> roles = form["Roles"].ToList();

            Console.WriteLine(firstName);
            Console.WriteLine(lastName);
            Console.WriteLine(email);
            Console.WriteLine(mobileNumber);
            Console.WriteLine("Size: " + roles.Count());
            // Employee employee1 = new Employee { FirstName = firstName, LastName = lastName, Email = email, Password = password, MobileNumber = mobileNumber };

            // foreach (string role in roles)
            // {
            //     Console.WriteLine(role);
            //     Role empRole = new Role { RoleName = role };
            //     employee1.Roles.Add(empRole);
            // }
            // _context.Employee.Add(employee1);
            // _context.SaveChanges();

            return View();
        }



        [HttpGet("removeEmployee")]
        public IActionResult RemoveEmployee()
        {
            TempData["msg"] = null;
            String user = HttpContext.Session.GetString("user");
            if (user == null)
            {
                return Redirect("/admin/signin");
            }
            var employee = _context.Employee
                .Include(e => e.Roles)
                .FirstOrDefault(e => e.Email == user);

            bool chq = false;
            foreach (Role r in employee.Roles)
            {
                if (r.RoleName.ToLower().Equals("owner"))
                {
                    chq = true;
                    break;
                }
                Console.WriteLine(r.RoleName);
            }
            if (chq == false)
            {
                return RedirectToAction("/employee/error");
            }
            else
            {
                return View(); ;
            }

            return View();
        }

        [HttpPost("removeEmployee")]
        public IActionResult RemoveEmployee(IFormCollection form)
        {
            string email = form["Email"];
            var employee = _context.Employee.Where(e => e.Email == email);
            if (employee == null)
            {
                TempData["msg"] = "No Employee Exist with this Email";
            }
            else
            {

                _context.Employee.Remove((Employee)employee);
                TempData["msg"] = "Employee Removed SuccessFully";
            }
            return View();
        }

        [HttpGet("addProduct")]
        public IActionResult addProduct()
        {
            TempData["msg"] = null;
            return View();

            String user = HttpContext.Session.GetString("user");
            if (user == null)
            {
                return Redirect("/admin/signin");
            }
            var employee = _context.Employee
                .Include(e => e.Roles)
                .FirstOrDefault(e => e.Email == user);

            bool chq = false;
            foreach (Role r in employee.Roles)
            {
                if (r.RoleName.ToLower().Equals("owner") || r.RoleName.ToLower().Equals("admin"))
                {
                    chq = true;
                    break;
                }
                Console.WriteLine(r.RoleName);
            }
            if (chq == false)
            {
                return RedirectToAction("/employee/error");
            }
            else
            {
                return View(); ;
            }

        }

        [HttpPost("addProduct")]
        public IActionResult addProduct(IFormCollection form)
        {

            string ProductName = form["ProductName"];
            string Category = form["Category"];
            string Description = form["Description"];
            IFormFile PrdImage = form.Files["ProductImage"];
            Console.WriteLine("Qksabsdnfbkjfn  " + ProductName);

            string PrdImgUrl = "";
            var product = new Products
            {
                ProductName = ProductName,
                Category = Category
                                        ,
                Description = Description,
                ProductImgUrl = PrdImgUrl
            };

            _context.Products.Add(product);
            _context.SaveChanges();
            TempData["msg"] = "Product has been added successfully!";
            return View();

        }
    }
}