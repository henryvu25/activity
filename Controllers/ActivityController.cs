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
    public class ActivityController : Controller
    {
        private BeltContext _context;
        public ActivityController(BeltContext context)
        {
            _context = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("home")]
        public IActionResult Home()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Activity> allActivities = _context.Activities.Include(w => w.Participants).ThenInclude(a => a.User).OrderBy(i => i.Date).ToList();
            List<User> users = _context.Users.ToList();
            ViewBag.users = users;
            ViewBag.activities = allActivities;
            ViewBag.sessionUserId = UserId;
           
            return View("Home");
        }
        [HttpGet]
        [Route("new")]
        public IActionResult New()
        {
             int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.actErrors = ModelState.Values;
            return View("New");
        }
        [HttpPost]
        [Route("create")]
        public IActionResult Create(ActivityViewModel model)
        {
            if (ModelState.IsValid)
            {
                int? UserId = HttpContext.Session.GetInt32("UserId");
                Activity NewActivity = new Activity
                {

                    Title = model.Title,
                    Date = model.Date,
                    Time = model.Time,
                    Duration = model.num + model.dur,
                    Description = model.Description,
                    UserId = UserId
                };
                _context.Activities.Add(NewActivity);
                _context.SaveChanges();
                int ActivityId = NewActivity.ActivityId;
                return Redirect($"/activity/{ActivityId}");
            }
            else
            {
                ViewBag.actErrors = ModelState.Values;
                return View("New");
            }

        }
        [HttpGet]
        [Route("activity/{id}")]
        public IActionResult Display(int id)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Activity displayedActivity = _context.Activities.Include(w => w.Participants).ThenInclude(a => a.User).Where(w => w.ActivityId == id).SingleOrDefault();
            ViewBag.activity = displayedActivity;
            List<User> users = _context.Users.ToList();
            ViewBag.users = users;
            ViewBag.sessionUserId = UserId;

            return View("Display");
        }
        [HttpGet]
        [Route("join/{id}")]
        public IActionResult Join(int id)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Activity displayedWedding = _context.Activities.Where(w => w.ActivityId == id).SingleOrDefault();
            User sessionUser = _context.Users.Where(u => u.UserId == UserId).SingleOrDefault();
            Join attending = new Join
            {
                UserId = UserId,
                ActivityId = id
            };
            _context.Joins.Add(attending);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }
        [HttpGet]
        [Route("leave/{id}")]
        public IActionResult Unrsvp(int id)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            User sessionUser = _context.Users.Where(u => u.UserId == UserId).SingleOrDefault();
            Join remove = _context.Joins.Where(u => u.UserId == UserId).SingleOrDefault(i => i.ActivityId == id);
            _context.Joins.Remove(remove);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }
        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            User sessionUser = _context.Users.Where(u => u.UserId == UserId).SingleOrDefault();
            Activity removeActivity = _context.Activities.Where(u => u.UserId == UserId).SingleOrDefault(i => i.ActivityId == id);
            _context.Activities.Remove(removeActivity);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }

    }
}