namespace ExpensesTracker.Models
{
    public class Category
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int? UserId { get; set; }
        public virtual ICollection<Item> Items { get; set; } = new HashSet<Item>();
        public virtual User? User { get; set; }
        public virtual Budget? Budget { get; set; }

    }
}
