namespace ForumSystem.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Security;

    using Microsoft.AspNet.Identity;

    using ForumSystem.Models;
    using ForumSystem.Data.Repositories;

    public class CategoryController : Controller
    {
        /// <summary>
        /// Queries the Category table and returns the category with the wanted id.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <param name="categories">Category repository.</param>
        /// <returns>Return the category with the wanted id.</returns>
        [NonAction]
        private Category GetCategoryById(int? id, IRepository<Category> categories)
        {
            var categoryById = categories
                .All()
                .Where(x => x.CategoryId == id)
                .FirstOrDefault();

            return categoryById;
        }

        [NonAction]
        private QuestionViewModel CreateNewQuestionViewModel(Question question)
        {
            return new QuestionViewModel
            {
                QuestionId = question.QuestionId,
                Title = question.Title,
                QuestionContent = question.QuestionContent,
                TimeOfCreation = question.TimeOfCreation,
                CategoryId = question.CategoryId,
                Answers = question.Answers,
                UserId = question.UserId, // Get the current user id
                User = question.User
            };
        }

        public ActionResult Index(int? id)
        {
            IRepository<Category> categories = new Repository<Category>();

            // If the id is not passed in the TempData - pass it, if so - skip it.
            // If this checking does not exist, the method might try to pass
            // the same id with the same key and that will cause an exception.
            if (!TempData.ContainsKey("CategoryID"))
            {
                TempData.Add("CategoryID", id);
            }
            else
            {
                TempData["CategoryId"] = id; // Change the categoryId if the user goes into another category.
            }

            var categoryById = GetCategoryById(id, categories);

            // Select all the questions in the current directory
            IList<Question> allQuestions = categoryById.Questions.ToList<Question>();

            IList<QuestionViewModel> listWithQuestionsInCategory = new List<QuestionViewModel>();

            for (int i = 0; i < allQuestions.Count; i++)
            {
                // Create view model with question
                var currentCategoryQuestions = CreateNewQuestionViewModel(allQuestions[i]);
                listWithQuestionsInCategory.Add(currentCategoryQuestions);
            }

            return View(listWithQuestionsInCategory.OrderByDescending(o => o.TimeOfCreation).ToList());
        } 

        /// <summary>
        /// The current user posts a new question.
        /// </summary>
        /// <param name="newQuestion">
        /// The needed information to create a new Question. The object is populated by the strings that are typed in the 
        /// textboxes and textfields.
        /// </param>
        /// <returns>Redirects to "Index" where the new question in fetched and displayed on the page.</returns>
        [HttpPost]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        public ActionResult PostQuestion(QuestionViewModel newQuestion)
        {
            int categoryId = (int)TempData.Values.ElementAt(0);
            if (ModelState.IsValid)
            {
                IRepository<Category> categories = new Repository<Category>();

                var categoryById = GetCategoryById(categoryId, categories);
                string userId = User.Identity.GetUserId();

                Question questionToBeAdded = new Question
                {
                    Title = newQuestion.Title,
                    QuestionContent = newQuestion.QuestionContent,
                    TimeOfCreation = DateTime.Now,
                    CategoryId = categoryId,
                    Category = categoryById,
                    UserId = userId,// Get the current user                    
                };

                // Save to database
                categoryById.Questions.Add(questionToBeAdded);
                categories.SaveChanges();
            }

            return RedirectToAction("Index", new { id = categoryId });
        }
    }
}