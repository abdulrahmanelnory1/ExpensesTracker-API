using ExpensesTracker.Models;
using ExpensesTracker.Repository;
using System.Security.Claims;
using ExpensesTracker.DTO;
using ExpensesTracker.DTO.BudgetDTOs;
using ExpensesTracker.Models;
using ExpensesTracker.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpensesTracker.Services
{
    public class RecurringExpenseService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<RecurringExpenseService> _logger;

        public RecurringExpenseService(IServiceProvider serviceProvider, ILogger<RecurringExpenseService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Recurring Expense Service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Run at the beginning of each day
                    var now = DateTime.Now;
                    var nextRun = now.Date.AddDays(1).AddHours(1); // Next day at 1 AM
                    var delay = nextRun - now;

                    _logger.LogInformation($"Next run at: {nextRun}");

                    await Task.Delay(delay, stoppingToken);

                    // Process regular expenses
                    await ProcessRegularExpenseService();
                }
                catch (TaskCanceledException)
                {
                    // Task was cancelled, likely due to application shutdown
                    _logger.LogInformation("Recurring Expense Service is stopping.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while processing recurring expenses.");
                }

            }
        }
        
        private async Task ProcessRegularExpenseService()
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ExpensesTrackerContext>();

            var RecurringExpenses = dbContext.RecurringExpenses
            .Where(re => re.IsActive && re.NextDueDate <= DateTime.Now)
            .ToList();

            try
            {
                foreach (var expense in RecurringExpenses)
                {
                    Expense newExpense = new Expense
                    {
                        Amount = expense.Amount,
                        Items = expense.Items,
                        User = expense.User,
                        Date = expense.Date,
                    };
                    // Update budgets for each item in the expense
                    foreach (Item item in newExpense.Items)
                    {
                        if (item.Category.Budget != null)
                        {
                            item.Category.Budget.CurrentLimit -= item.Price;
                        }
                    }
                    // Deduct the expense amount from the user's balance
                    expense.User.Balance -= expense.Amount;

                    // Add the new expense to the database
                    dbContext.Expenses.Add(newExpense);

                    // Update the last processed date of the regular expense
                    expense.LastProcessedDate = DateTime.Now;

                    // Update the next due date based on the frequency
                    switch (expense.Frequency)
                    {
                        case Frequency.Daily:
                            expense.NextDueDate = expense.NextDueDate.AddDays(1);
                            break;
                        case Frequency.Weekly:
                            expense.NextDueDate = expense.NextDueDate.AddDays(7);
                            break;
                        case Frequency.Monthly:
                            expense.NextDueDate = expense.NextDueDate.AddMonths(1);
                            break;
                        case Frequency.Yearly:
                            expense.NextDueDate = expense.NextDueDate.AddYears(1);
                            break;
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing a regular expense.");
            }


        }
    }
}
