using ExpensesTracker.DTO;
using ExpensesTracker.Models;
using ExpensesTracker.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExpensesTracker.Repository;
public class ExpenseRepo : GenericRepo<Expense>
{

    public ExpenseRepo(ExpensesTrackerContext _db) : base(_db)
    {
    }

    public List<Expense> getByUserId(int userId)
    {
        return db.Expenses.Where(e => e.UserId == userId).ToList();
    }

    public List<Expense> getByDate(int userID, DateOnly date)
    {
        return db.Expenses
        .Where(e => e.UserId == userID &&
                    DateOnly.FromDateTime(e.Date) <= DateOnly.FromDateTime(DateTime.Today) &&
                    DateOnly.FromDateTime(e.Date) >= date)
        .ToList();
    }

    public List<Expense> sortByDate()
    {
        return db.Expenses.OrderBy(e => e.Date).ToList();
    }

    public List<Item> getItemsByExpenseId(int expenseId)
    {
        Expense expense =  db.Expenses
       .Include(e => e.Items)
       .FirstOrDefault(e => e.Id == expenseId);

        return expense?.Items.ToList();
    }

}

