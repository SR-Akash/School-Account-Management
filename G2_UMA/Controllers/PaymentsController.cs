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
    public class PaymentsController : Controller
    {
        private readonly dataContext _context;

        public PaymentsController(dataContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Payment.Include(p => p.Fees).Include(p => p.Months).Include(p => p.Students);
            return View(await dataContext.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .Include(p => p.Fees)
                .Include(p => p.Months)
                .Include(p => p.Students)
                .FirstOrDefaultAsync(m => m.id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            ViewData["Fee_Id"] = new SelectList(_context.Fees, "Fee_Id", "F_Name");
            ViewData["M_Id"] = new SelectList(_context.Months, "M_Id", "M_Name");
            ViewData["Std_Id"] = new SelectList(_context.Students, "Std_Id", "Std_Id");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Std_Id,Fee_Id,Amount,M_Id")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Fee_Id"] = new SelectList(_context.Fees, "Fee_Id", "F_Name", payment.Fee_Id);
            ViewData["M_Id"] = new SelectList(_context.Months, "M_Id", "M_Name", payment.M_Id);
            ViewData["Std_Id"] = new SelectList(_context.Students, "Std_Id", "Std_Id", payment.Std_Id);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["Fee_Id"] = new SelectList(_context.Fees, "Fee_Id", "F_Name", payment.Fee_Id);
            ViewData["M_Id"] = new SelectList(_context.Months, "M_Id", "M_Name", payment.M_Id);
            ViewData["Std_Id"] = new SelectList(_context.Students, "Std_Id", "Std_Id", payment.Std_Id);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Std_Id,Fee_Id,Amount,M_Id")] Payment payment)
        {
            if (id != payment.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.id))
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
            ViewData["Fee_Id"] = new SelectList(_context.Fees, "Fee_Id", "F_Name", payment.Fee_Id);
            ViewData["M_Id"] = new SelectList(_context.Months, "M_Id", "M_Name", payment.M_Id);
            ViewData["Std_Id"] = new SelectList(_context.Students, "Std_Id", "Std_Id", payment.Std_Id);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .Include(p => p.Fees)
                .Include(p => p.Months)
                .Include(p => p.Students)
                .FirstOrDefaultAsync(m => m.id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payment.FindAsync(id);
            _context.Payment.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payment.Any(e => e.id == id);
        }
    }
}
