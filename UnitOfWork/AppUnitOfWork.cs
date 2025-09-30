using ExpensesTracker.Models;
using ExpensesTracker.Repository;

namespace ExpensesTracker.UnitOfWork
{
    public class AppUnitOfWork
    {
        UserRepo userRepo;
        CategoryRepo categoryRepo;
        GenericRepo<Budget> budgetRepo;
        GenericRepo<Item> itemRepo;
        ExpenseRepo expenseRepo;
        RecurringExpenseRepo recurringExpenseRepo;
        ExpensesTrackerContext db;

        public AppUnitOfWork(ExpensesTrackerContext db)
        {
            this.db = db;
        }

        public UserRepo UserRepo
        {
            get
            {
                if (userRepo == null)
                {
                    userRepo = new UserRepo(db);
                }
                return userRepo;
            }
        }

        public CategoryRepo CategoryRepo
        {
            get
            {
                if (categoryRepo == null)
                {
                    categoryRepo = new CategoryRepo(db);
                }
                return categoryRepo;
            }
        }

        public GenericRepo<Budget> BudgetRepo
        {
            get
            {
                if (budgetRepo == null)
                {
                    budgetRepo = new GenericRepo<Budget>(db);
                }
                return budgetRepo;
            }
        }

        public GenericRepo<Item> ItemRepo
        {
            get
            {
                if (itemRepo == null)
                {
                    itemRepo = new GenericRepo<Item>(db);
                }
                return itemRepo;
            }
        }

        public ExpenseRepo ExpenseRepo
        {
            get
            {
                if (expenseRepo == null)
                {
                    expenseRepo = new ExpenseRepo(db);
                }
                return expenseRepo;
            }
        }

        public RecurringExpenseRepo RecurringExpenseRepo
        {
            get
            {
                if (recurringExpenseRepo == null)
                {
                    recurringExpenseRepo = new RecurringExpenseRepo(db);
                }
                return recurringExpenseRepo;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
