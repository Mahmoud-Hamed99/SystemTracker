using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker_2.Models
{
    public class ExpenseDBContext : DbContext
    {
        public ExpenseDBContext()
        {
        }

        public ExpenseDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }
        public  DbSet<ExpenseCategory> ExpensesCategory { get; set;}
    }
}
