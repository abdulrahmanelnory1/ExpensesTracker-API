using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Models.Configuration
{
    public class RegularExpenseConfig : IEntityTypeConfiguration<RecurringExpense>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<RecurringExpense> builder)
        {
            builder.Property(re => re.Frequency).IsRequired();
            builder.Property(re => re.IsActive)
                .IsRequired();
            builder.Property(re => re.NextDueDate);
            builder.Property(re => re.LastProcessedDate);
        }

    }

    
}
