using ExpensesTracker.Models;
using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.DTO.BudgetDTOs
{
    public class RequestBudgetDTO
    {
        [Required]
        public decimal Limit { get; set; }
        public DateOnly StartDate { get; set; }
        [Required]
        public DateOnly EndDate { get; set; }
        //public int CategoryId { get; set; }
    }
}
