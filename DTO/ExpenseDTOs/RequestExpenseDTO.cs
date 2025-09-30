using ExpensesTracker.DTO.ItemDTOs;
using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.DTO.ExpenseDTOs
{
    public class RequestExpenseDTO
    {
        [Required]
        public DateTime Date { get; set; }

        public List<RequestItemDTO> Items { get; set; } = new List<RequestItemDTO>();
    }
}
