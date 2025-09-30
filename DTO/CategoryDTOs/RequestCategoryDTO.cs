using ExpensesTracker.Models;
using System.ComponentModel.DataAnnotations;
using ExpensesTracker.DTO;
using ExpensesTracker.DTO.BudgetDTOs;

namespace ExpensesTracker.DTO.CategoryDTOs
{
    public class RequestCategoryDTO
    {
        [Required]
        public string Name { get; set; }
        public RequestBudgetDTO? Budget { get; set; }

    }
}