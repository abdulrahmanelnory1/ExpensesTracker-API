using ExpensesTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Models.Configuration
{
    public class ItemConfig: IEntityTypeConfiguration<Item>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Name).IsRequired().HasMaxLength(100); 
            builder.Property(i => i.Price).IsRequired().HasPrecision(18, 2);

            builder.HasOne(i => i.Category)
                   .WithMany(c => c.Items)
                   .HasForeignKey(i => i.CategoryId).IsRequired()
                   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);// When a Category is deleted, all its associated Items are assigned to the "Other" Category

            builder.Property(i => i.ExpenseId).IsRequired();

            builder.HasOne(item => item.Expense)   
                   .WithMany(expense => expense.Items)     
                   .HasForeignKey(item => item.ExpenseId) 
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
