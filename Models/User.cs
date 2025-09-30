using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace ExpensesTracker.Models
{
    public class User
    {    
        public int Id { get; set; }
        
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; }
        public decimal Balance { get; set; }
        public decimal Income { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; } = new HashSet<Expense>();
        public virtual ICollection<Budget> Budgets { get; set; } = new HashSet<Budget>();
        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();

    }
}
