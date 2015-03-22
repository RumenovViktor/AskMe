namespace ForumSystem.Data
{
    using System;
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using ForumSystem.Models;
    using ForumSystem.Data.Interfaces;
    using ForumSystem.Data.Migrations;

    public class ForumDbContext : IdentityDbContext<User>, IForumDbContext
    {
        public ForumDbContext() : base("ForumDB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ForumDbContext, Configuration>());
        }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<Question> Questions { get; set; }

        public IDbSet<Answer> Answers { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public static ForumDbContext Create()
        {
            return new ForumDbContext();
        }
    }
}
