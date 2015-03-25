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
        [NonAction]
        private Category GetCategoryById(int? id, IRepository<Category> categories)
        {
            var categoryById = categories
                .All()
                .Where(x => x.CategoryId == id)
                .FirstOrDefault();

            return categoryById;
        }

        public ActionResult Index(int? id)
        {
            if (!TempData.ContainsKey("CategoryID"))
            {
                TempData.Add("CategoryID", id);
            }

            IRepository<Category> categories = new Repository<Category>();

            var categoryById = GetCategoryById(id, categories);

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
                    CategoryId = allQuestions[i].CategoryId,
                    Answers = allQuestions[i].Answers
                };

                listWithQuestionsInCategory.Add(currentCategoryQuestions);

            }

            return View(listWithQuestionsInCategory);
        }

        [HttpPost]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        public ActionResult PostQuestion(QuestionViewModel newQuestion)
        {
            IRepository<Category> categories = new Repository<Category>();

            int categoryId = (int)TempData.Values.ElementAt(0);

            //var errors = ModelState
            //            .Where(x => x.Value.Errors.Count > 0)
            //            .Select(x => new { x.Key, x.Value.Errors })
            //            .ToArray();

            if (ModelState.IsValid)
            {
                //TODO: Save to database
                var categoryById = GetCategoryById(categoryId, categories);
                categoryById.Questions.Add(new Question 
                {
                    Title = newQuestion.Title,
                    QuestionContent = newQuestion.QuestionContent,
                    TimeOfCreation = DateTime.Now,
                    CategoryId = categoryId                    
                });

                categories.SaveChanges();
            }

            return RedirectToAction("Index", new { id = categoryId });
        }
    }
}