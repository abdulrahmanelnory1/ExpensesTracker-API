using ExpensesTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.Models.Configuration
{


    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(60);
            builder.Property(u => u.Password).IsRequired();
            builder.Property(u => u.Income).IsRequired().HasPrecision(18, 2);
            builder.Property(u => u.Balance).IsRequired().HasPrecision(18, 2);
        }
    }
}
