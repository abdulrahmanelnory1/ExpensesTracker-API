using ExpensesTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ExpensesTracker.Repository
{
    public class RecurringExpenseRepo : GenericRepo<RecurringExpense>
    {
        ExpensesTrackerContext _db;
        public RecurringExpenseRepo(ExpensesTrackerContext _db) : base(_db)
        {
           
        }

        public async Task<List<RecurringExpense>> GetByUserId(int userId)
        {
            return await db.RecurringExpenses.Where(e => e.UserId == userId && e.IsActive && e.NextDueDate <= DateTime.Now).ToListAsync();
        }
    }
}
