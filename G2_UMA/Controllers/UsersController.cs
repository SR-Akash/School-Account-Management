using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using G2_UMA.Data;
using G2_UMA.Models;

namespace G2_UMA.Controllers
{
    public class UsersController : Controller
    {
        private readonly dataContext _context;

        public UsersController(dataContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser([Bind("username,pass,type")] User logins)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logins);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(logins);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(User u)
        {
            var test = _context.User.Where(d => d.username == u.username && d.pass == u.pass && d.type == u.type).FirstOrDefault();
            if (test != null)
            {
                if (test.type == "Admin")
                {
                    return RedirectToAction("Index", "Users");
                }
                else if (test.type == "Student")
                {
                    return RedirectToAction("Details", "Students", new { username = test.username });
                }
                else if (test.type == "Accounts")
                {
                    return RedirectToAction("Index", "Payments");
                }
            }
            else
            {
                ViewBag.msg = "Username or Password or Type is incorrect";
            }
            return View();
        }
        public async Task<IActionResult> StudentDetails(string id, string type)
        {

            if (type == "Student")
            {
                var studentInfo = _context.StudentInfo.Where(s => s.std_id == id).FirstOrDefault();
                return View("StudentDetails");
            }
            else if (type == "Admin")
            {
                var user = _context.User.Where(u => u.username == id).FirstOrDefault();
                return View("Details", user);
            }
            else
            {
                return View();
            }

        }
        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.username == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("username,pass,type")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.SingleOrDefaultAsync(m => m.username == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("username,pass,type")] User user)
        {
            if (id != user.username)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.username))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.username == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.username == id);
        }
    }
}
