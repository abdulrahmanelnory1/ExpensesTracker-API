using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensesTracker.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public virtual User User { get; set; }
        public virtual ICollection<Item> Items { get; set; } = new HashSet<Item>();

    }
}
