namespace ForumSystem.Data.Interfaces
{
    using System;
    using System.Data.Entity;

    using ForumSystem.Models;

    public interface IForumDbContext
    {
        IDbSet<Category> Categories { get; set; }

        IDbSet<Question> Questions { get; set; }

        IDbSet<Answer> Answers { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();
    }
}
