using ExpensesTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Models.Configuration
{
    public class CategoryConfig:IEntityTypeConfiguration<Category>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(50);

            builder.HasOne(c => c.User)
                   .WithMany(u => u.Categories)
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.NoAction);// handle it manually: delete user -> delete all categories he created -> assign all this categories items to "Other" Category 
        }
    }
}
