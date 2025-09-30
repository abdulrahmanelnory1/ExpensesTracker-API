using ExpensesTracker.DTO.BudgetDTOs;
using System.Text.Json.Serialization;
namespace ExpensesTracker.DTO.CategoryDTOs
{
    public class ResponseCategoryDTO
    {
        public string Name { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ResponseBudgetDTO? Budget { get; set; }

    }
}
