namespace ForumSystem.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using ForumSystem.Models;
    using ForumSystem.Data.Repositories;

    public class CategoryController : Controller
    {
        public ActionResult Index(int? id) 
        {
            IRepository<Category> categories = new Repository<Category>();

            // Select the category by id
            var categoryById = categories
                .All()
                .Where(x => x.CategoryId == id)
                .FirstOrDefault();

            // Select all the questions in the current directory
            IList<Question> allQuestions = new List<Question>();
            allQuestions = categoryById.Questions.ToList<Question>();

            IList<QuestionViewModel> listWithQuestionsInCategory = new List<QuestionViewModel>();

            for (int i = 0; i < allQuestions.Count; i++)
            {
                // Create view model with question
                var currentCategoryQuestions = new QuestionViewModel 
                {
                    Title = allQuestions[i].Title,
                    QuestionContent = allQuestions[i].QuestionContent,
                    TimeOfCreation = allQuestions[i].TimeOfCreation,
                    Answers = allQuestions[i].Answers
                };

                listWithQuestionsInCategory.Add(currentCategoryQuestions);

            }

            return View(listWithQuestionsInCategory);
        }

        [HttpPost]
        public ActionResult PostQuestion(QuestionViewModel newQuestion)
        {
            return null;
        }
    }
}