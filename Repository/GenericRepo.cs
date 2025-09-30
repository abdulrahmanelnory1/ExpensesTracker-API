using ExpensesTracker.Models;
using ExpensesTracker.Models;

namespace ExpensesTracker.Repository
{
    public class GenericRepo<TEntity> where TEntity : class
    {
        protected ExpensesTrackerContext db;

        public GenericRepo(ExpensesTrackerContext _db)
        {
            this.db = _db;
        }
        
        public List<TEntity> GetAll() =>
            db.Set<TEntity>().ToList();
        public TEntity getById(int id) =>
            db.Set<TEntity>().Find(id);

        //public void Save()=>
        //    db.SaveChanges();

        public void Add(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            db.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            db.Set<TEntity>().Update(entity);
        }
    }
}
