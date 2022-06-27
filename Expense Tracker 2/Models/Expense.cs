using System;

namespace Expense_Tracker_2.Models
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        public int ExpenseCategoryId { get; set; }
        public ExpenseCategory expenseCategory { get; set; }
        public DateTime? ExpenseDate { get; set; }
        public double? Amount { get; set; }
    }
}
