using System.Text.Json.Serialization;

namespace ExpensesTracker.DTO.BudgetDTOs
{
    public class ResponseBudgetDTO
    {
        public decimal Limit { get; set; }
        public decimal CurrentLimit { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
