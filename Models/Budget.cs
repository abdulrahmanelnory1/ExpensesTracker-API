using System.Reflection.PortableExecutable;

namespace ExpensesTracker.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Limit { get; set; }
        public decimal CurrentLimit { get; set; }
        public DateOnly? StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        public DateOnly EndDate { get; set; }
        public int CategoryId { get; set; }    
        public virtual Category Category { get; set; }
        public virtual User User { get; set; }

    }
}
