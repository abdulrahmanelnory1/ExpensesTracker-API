using ExpensesTracker.Models;

namespace ExpensesTracker.Repository
{
    public class UserRepo:GenericRepo<User>
    {
        public UserRepo(ExpensesTrackerContext _db):base(_db)
        {
        }

        public User getByEmail(string email) =>        
             db.Users.FirstOrDefault(u => u.Email == email);

       
    }
   
}
