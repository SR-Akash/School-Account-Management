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
    public class StudentsController : Controller
    {
        private readonly dataContext _context;

        public StudentsController(dataContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Students.Include(s => s.Months);
            return View(await dataContext.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(string username)
        {
            if (username == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .Include(s => s.Months)
                .FirstOrDefaultAsync(m => m.Std_Id == username);
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        public IActionResult Home(string username)
        {
            ViewData["username"] = username;
            return View();
        }

        public IActionResult Show(string username, int month)
        {
            var test = _context.Payment.Where(p => p.Std_Id == username && p.Fee_Id == 2).SingleOrDefault();
            if (test == null)
            {
                return NotFound();
            }

            int temp = month - test.M_Id;
            int temp2 = 1000 - test.Amount;
            int res = (1000 * temp) + (200 * (temp - 1)) + temp2;
            if (res < 0) res = 0;
            ViewBag.pay = res;
            return View();
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["M_Id"] = new SelectList(_context.Months, "M_Id", "M_Name");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Std_Id,Name,Birth_Date,Gender,Contact,Address,Father_Name,Mother_Name,Class,M_Id")] Students students)
        {
            if (ModelState.IsValid)
            {
                _context.Add(students);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["M_Id"] = new SelectList(_context.Months, "M_Id", "M_Name", students.M_Id);
            return View(students);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = await _context.Students.FindAsync(id);
            if (students == null)
            {
                return NotFound();
            }
            ViewData["M_Id"] = new SelectList(_context.Months, "M_Id", "M_Name", students.M_Id);
            return View(students);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Std_Id,Name,Birth_Date,Gender,Contact,Address,Father_Name,Mother_Name,Class,M_Id")] Students students)
        {
            if (id != students.Std_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(students);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentsExists(students.Std_Id))
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
            ViewData["M_Id"] = new SelectList(_context.Months, "M_Id", "M_Name", students.M_Id);
            return View(students);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .Include(s => s.Months)
                .FirstOrDefaultAsync(m => m.Std_Id == id);
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var students = await _context.Students.FindAsync(id);
            _context.Students.Remove(students);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentsExists(string id)
        {
            return _context.Students.Any(e => e.Std_Id == id);
        }
    }
}
