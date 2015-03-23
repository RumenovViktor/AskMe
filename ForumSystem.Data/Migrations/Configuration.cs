namespace ForumSystem.Data.Migrations
{
    using System;
    using System.Linq;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using ForumSystem.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ForumDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ForumDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Categories.AddOrUpdate(
                c => c.CategoryName,
                new Category { CategoryName = "iOS" },
                new Category { CategoryName = "Android" },
                new Category { CategoryName = "Quality Assurance" },
                new Category { CategoryName = "ASP.NET MVC" },
                new Category { CategoryName = "WebForms" },
                new Category { CategoryName = "Single Page Applications" },
                new Category { CategoryName = "Windows Forms" });
        }
    }
}
