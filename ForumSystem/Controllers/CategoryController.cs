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

            if (!TempData.ContainsKey("CategoryID"))
            {
                TempData.Add("CategoryID", id);
            }

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
            int categoryId = (int)TempData.Values.ElementAt(0);


            if (ModelState.IsValid)
            {
                //TODO: Save to database
            }

            return RedirectToAction("Index", categoryId);
        }
    }
}