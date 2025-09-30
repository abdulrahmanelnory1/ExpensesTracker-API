using ExpensesTracker.Models;
using ExpensesTracker.Models.Configuration;

using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Models
{
    public class ExpensesTrackerContext : DbContext
    {
        public ExpensesTrackerContext()
        {
        }

        public ExpensesTrackerContext(DbContextOptions<ExpensesTrackerContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<RecurringExpense> RecurringExpenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExpensesTrackerContext).Assembly);
            modelBuilder.Entity<Expense>(a => { a.UseTptMappingStrategy(); });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
