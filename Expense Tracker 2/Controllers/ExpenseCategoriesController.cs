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
    public class ExpenseCategoriesController : Controller
    {
        private readonly ExpenseDBContext _context;

        public ExpenseCategoriesController(ExpenseDBContext context)
        {
            _context = context;
        }

        // GET: ExpenseCategories
        public async Task<IActionResult> Index()
        {
              return View(await _context.ExpensesCategory.ToListAsync());
        }

        // GET: ExpenseCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ExpensesCategory == null)
            {
                return NotFound();
            }

            var expenseCategory = await _context.ExpensesCategory
                .FirstOrDefaultAsync(m => m.ExpenseCategoryId == id);
            if (expenseCategory == null)
            {
                return NotFound();
            }

            return View(expenseCategory);
        }

        // GET: ExpenseCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExpenseCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExpenseCategoryId,ExpenseCategoryName")] ExpenseCategory expenseCategory)
        {
            if (ModelState.IsValid)
            {
                var categoryName=_context.ExpensesCategory.Where(x => x.ExpenseCategoryName.Equals(expenseCategory.ExpenseCategoryName)).FirstOrDefault();
               
                if (categoryName == null)
                {
                _context.Add(expenseCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                }

            }
            return RedirectToAction(nameof(Index));

        }

        // GET: ExpenseCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ExpensesCategory == null)
            {
                return NotFound();
            }

            var expenseCategory = await _context.ExpensesCategory.FindAsync(id);
            if (expenseCategory == null)
            {
                return NotFound();
            }
            return View(expenseCategory);
        }

        // POST: ExpenseCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExpenseCategoryId,ExpenseCategoryName")] ExpenseCategory expenseCategory)
        {
            if (id != expenseCategory.ExpenseCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var categoryName = _context.ExpensesCategory.Where(x => x.ExpenseCategoryName.Equals(expenseCategory.ExpenseCategoryName)).FirstOrDefault();

                    if (categoryName == null)
                    { 
                    _context.Update(expenseCategory);
                    await _context.SaveChangesAsync();
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseCategoryExists(expenseCategory.ExpenseCategoryId))
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
            return View(expenseCategory);
        }

        // GET: ExpenseCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ExpensesCategory == null)
            {
                return NotFound();
            }

            var expenseCategory = await _context.ExpensesCategory
                .FirstOrDefaultAsync(m => m.ExpenseCategoryId == id);
            if (expenseCategory == null)
            {
                return NotFound();
            }

            return View(expenseCategory);
        }

        // POST: ExpenseCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ExpensesCategory == null)
            {
                return Problem("Entity set 'ExpenseDBContext.ExpensesCategory'  is null.");
            }
            var expenseCategory = await _context.ExpensesCategory.FindAsync(id);
            if (expenseCategory != null)
            {
                _context.ExpensesCategory.Remove(expenseCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseCategoryExists(int id)
        {
          return _context.ExpensesCategory.Any(e => e.ExpenseCategoryId == id);
        }
    }
}
