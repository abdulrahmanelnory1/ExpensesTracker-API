using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.DTO
{
    public class RequestItemDTO
    {
        [Required]
        public decimal Price { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]
        
        public int CategoryId { get; set; }


    }
}
