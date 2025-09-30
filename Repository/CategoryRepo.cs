using ExpensesTracker.Models;
using ExpensesTracker.Models;

namespace ExpensesTracker.Repository
{
    public class CategoryRepo: GenericRepo<Category>
    {
        public CategoryRepo(ExpensesTrackerContext _db) : base(_db)
        {
        }

        public Category GetByName(string name) =>
            db.Categories.FirstOrDefault(c => c.Name == name);

        public List<Item> GetItems(int id)
        {
            return db.Items.Where(i => i.CategoryId == id).ToList();
        }

        public List<Category> GetAll(int id)
        {
            return db.Categories.Where(c => c.UserId == null || (c.UserId != null && c.UserId == id)).ToList();
        }
    }
}
