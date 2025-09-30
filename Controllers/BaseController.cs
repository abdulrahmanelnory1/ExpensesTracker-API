using ExpensesTracker.Models;
using ExpensesTracker.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Claims;

namespace ExpensesTracker.Controllers
{
    public class BaseController : ControllerBase
    {
        protected bool validBudget(Budget budget, DateTime date)
        {
            return budget.StartDate <= DateOnly.FromDateTime(date) && budget.EndDate >= DateOnly.FromDateTime(date);
        }
        protected int? getUserId()
        {

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userIdClaim, out int userId))
                return userId;

            return null;
        }
    }
}
