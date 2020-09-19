using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using G2_UMA.Data;
using G2_UMA.Models;

namespace G2_UMA.Controllers
{
    public class HomeController : Controller
    {

        private readonly dataContext _context;
        public HomeController(dataContext context)
        {
            _context = context;
        }

        public IActionResult Faculty()
        {
            var test = _context.Students.FirstOrDefault();
            Students model = new Students
            {
                Std_Id = test.Std_Id,
                Name = test.Name
            };
            return View(model);
        }
        public IActionResult Student()
        {
            var test = _context.Students;
            var model = new List<Students>();
            foreach (var i in test)
            {
                var s = new Students();
                s.Std_Id = i.Std_Id;
                s.Name = i.Name;
                model.Add(s);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        //[Route("Home/StudentLogin")]
        public IActionResult Index(Students std)
        {
            var test = _context.Students;
            var model = new List<Students>();
            var i = _context.Students.Where(x => x.Std_Id == std.Std_Id && x.Name == std.Name).FirstOrDefault();
            if (i == null)
            {
                ViewBag.Login = "Data Does not exist";
            }
            else
            {
                ViewBag.Login = "Data Found";

                var s = new Students();
                s.Std_Id = i.Std_Id;
                s.Name = i.Name;
                model.Add(s);

                return View("SDetails", model);
            }
            return View();
        }


        [HttpPost]
        [Route("Home/StudentDetails")]
        public IActionResult Accounts()
        {

            return View();

        }

        public IActionResult StudentLogin()
        {

            ViewData["Message"] = "Your Msg";

            return View();

        }
        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
