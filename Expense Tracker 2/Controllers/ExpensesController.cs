using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expense_Tracker_2.Models;

namespace Expense_Tracker_2.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly ExpenseDBContext _context;

        public ExpensesController(ExpenseDBContext context)
        {
            _context = context;
        }

        // GET: Expenses
        public async Task<IActionResult> Index()
        {
            var expenseDBContext =await _context.Expenses.Include(e => e.expenseCategory).ToListAsync();

            return View(expenseDBContext);
        }

        public async Task<IActionResult> IndexWithDates(DateTime? startDate, DateTime? endDate)
        {
            var expenseDBContext = new List<Expense>();
            if (startDate == null && endDate == null)
                expenseDBContext = await _context.Expenses.Include(e => e.expenseCategory).ToListAsync();
            else if (startDate != null && endDate == null)
                expenseDBContext = await _context.Expenses.Include(e => e.expenseCategory).Where(a => a.ExpenseDate >= startDate).ToListAsync();
            else if (startDate == null && endDate != null)
                expenseDBContext = await _context.Expenses.Include(e => e.expenseCategory).Where(a => a.ExpenseDate <= endDate).ToListAsync();
            else
                expenseDBContext = await _context.Expenses.Include(e => e.expenseCategory).Where(a => a.ExpenseDate >= startDate && a.ExpenseDate <= endDate).ToListAsync();

            return PartialView("_Expenses",expenseDBContext);
        }

        // GET: Expenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Expenses == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses
                .Include(e => e.expenseCategory)
                .FirstOrDefaultAsync(m => m.ExpenseId == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // GET: Expenses/Create
        public IActionResult Create()
        {
            ViewData["ExpenseCategoryId"] = new SelectList(_context.ExpensesCategory, "ExpenseCategoryId", "ExpenseCategoryName");
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExpenseId,ExpenseCategoryId,ExpenseDate,Amount")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExpenseCategoryId"] = new SelectList(_context.ExpensesCategory, "ExpenseCategoryId", "ExpenseCategoryName", expense.ExpenseCategoryId);
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Expenses == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            ViewData["ExpenseCategoryId"] = new SelectList(_context.ExpensesCategory, "ExpenseCategoryId", "ExpenseCategoryName", expense.ExpenseCategoryId);
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExpenseId,ExpenseCategoryId,ExpenseDate,Amount")] Expense expense)
        {
            if (id != expense.ExpenseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expense.ExpenseId))
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
            ViewData["ExpenseCategoryId"] = new SelectList(_context.ExpensesCategory, "ExpenseCategoryId", "ExpenseCategoryName", expense.ExpenseCategoryId);
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Expenses == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses
                .Include(e => e.expenseCategory)
                .FirstOrDefaultAsync(m => m.ExpenseId == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Expenses == null)
            {
                return Problem("Entity set 'ExpenseDBContext.Expenses'  is null.");
            }
            var expense = await _context.Expenses.FindAsync(id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseExists(int id)
        {
          return _context.Expenses.Any(e => e.ExpenseId == id);
        }
    }
}
