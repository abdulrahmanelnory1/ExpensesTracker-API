namespace ExpensesTracker.Models
{
    public class RecurringExpense : Expense
    {
        public DateTime NextDueDate { get; set; }
        public bool IsActive { get; set; } = true;
        public Frequency Frequency { get; set; }
        public DateTime? LastProcessedDate { get; set; }

    }
}
