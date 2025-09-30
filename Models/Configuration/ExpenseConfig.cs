using ExpensesTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Models.Configuration
{
    public class ExpenseConfig : IEntityTypeConfiguration<Expense>
    {

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Expense> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Amount).IsRequired().HasPrecision(18, 2);
            builder.Property(e => e.Date);

            builder.HasOne(e => e.User)
                   .WithMany(u => u.Expenses)
                   .HasForeignKey(e => e.UserId).IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
