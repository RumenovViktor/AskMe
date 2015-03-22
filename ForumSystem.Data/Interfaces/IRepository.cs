namespace ForumSystem.Data.Repositories
{
    using System;
    using System.Linq;
 
    public interface IRepository<T> 
    {
        IQueryable<T> All();

        void Add(T entity);

        void Delete(T entity);

        void SaveChanges();
    }
}
