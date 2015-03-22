namespace ForumSystem.Data.Repositories
{
    using System;
    using System.Linq;

    using ForumSystem.Data.Interfaces;

    public class Repository<T> : IRepository<T> where T : class
    {
        private IForumDbContext db;

        public Repository() 
        {
            this.db = new ForumDbContext();
        }

        /// <summary>
        /// Get all the records in the database for the given model.
        /// </summary>
        /// <returns>All records in the database.</returns>
        public IQueryable<T> All()
        {
            return db.Set<T>();
        }

        /// <summary>
        /// Add a new record to the database.
        /// </summary>
        /// <param name="entity">The record you want to add.</param>
        public void Add(T entity)
        {
            this.db.Set<T>().Add(entity);
        }

        /// <summary>
        /// Delete a record from the database
        /// </summary>
        /// <param name="entity">The record you want to delete.</param>
        public void Delete(T entity)
        {
            this.db.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Save the changes to database.
        /// </summary>
        public void SaveChanges()
        {
            this.db.SaveChanges();
        }
    }
}
