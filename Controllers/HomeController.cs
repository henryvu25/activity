using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using belt_project.Models;
using System.Linq;

namespace belt_project.Controllers
{
    public class HomeController : Controller
    {
        private BeltContext _context;
        public HomeController(BeltContext context)
        {
            _context = context;
        }
        // GET: /Home/
       [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.invalid = TempData["Invalid"];
            ViewBag.errors = ModelState.Values;
            return View("Index");
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                User NewUser = new User
                {
                    First = model.First,
                    Last = model.Last,
                    Email = model.Email,
                    Password = model.Password,
                    Created = DateTime.Now,
                    Updated = DateTime.Now
                };
                _context.Users.Add(NewUser);
                _context.SaveChanges();
                User ReturnedUser = _context.Users.SingleOrDefault(user => user.Email == model.Email);
                HttpContext.Session.SetInt32("UserId", ReturnedUser.UserId);
            }
            else
            {
                ViewBag.errors = ModelState.Values;
                return View("Index");  
            }
            return RedirectToAction("Home", "Activity");
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(User model)
        {
            User ReturnedUser = _context.Users.SingleOrDefault(user => user.Email == model.Email);
            if(ReturnedUser.Password == model.Password)
            {
            HttpContext.Session.SetInt32("UserId", ReturnedUser.UserId);
            return RedirectToAction("Home", "Activity");
            }
            else
            {
                TempData["Invalid"] = "Invalid Email/Password";
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
