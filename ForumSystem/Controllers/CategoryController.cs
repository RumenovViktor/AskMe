namespace ForumSystem.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Linq;
    using System.Linq.Expressions;

    using ForumSystem.Models;
    using ForumSystem.Data.Repositories;

    public class CategoryController : Controller
    {
        public ActionResult Index(int? id) 
        {
            IRepository<Category> questions = new Repository<Category>();

            // Select all the questions that are in the wanted category
            var questionsById = questions
                .All()
                .Where(x => x.CategoryId == id)
                .Select(x => x.Questions);

            return View(questionsById);
        }
    }
}