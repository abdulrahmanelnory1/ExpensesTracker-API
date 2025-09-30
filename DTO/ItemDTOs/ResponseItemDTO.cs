using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.DTO.ItemDTOs
{
    public class ResponseItemDTO
    {
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
    }
}
