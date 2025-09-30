using ExpensesTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Models.Configuration
{
    public class BudgetConfig : IEntityTypeConfiguration<Budget>
    {

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Budget> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Limit).IsRequired().HasPrecision(18, 2);

            builder.Property(b => b.StartDate);

            builder.Property(b => b.CurrentLimit)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");
                   

            builder.Property(b => b.EndDate).IsRequired().HasColumnType("date");

            builder.HasOne(b => b.User)
                   .WithMany(u => u.Budgets)
                   .HasForeignKey(b => b.UserId).IsRequired()
                   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);

            builder.HasOne(b => b.Category)
                   .WithOne(c => c.Budget)
                   .HasForeignKey<Budget>(b => b.CategoryId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
