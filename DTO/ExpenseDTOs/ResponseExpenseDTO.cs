using ExpensesTracker.Models;
using ExpensesTracker.DTO.ItemDTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensesTracker.DTO.ExpenseDTOs
{
    public class ResponseExpenseDTO
    {
        [Required]
        public decimal amount { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public List<ResponseItemDTO> items { get; set; } = new List<ResponseItemDTO>();


    }
}
