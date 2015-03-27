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
        private IRepository<User> users = new Repository<User>();
        private IRepository<Category> categories = new Repository<Category>();

        /// <summary>
        /// Queries the Category table and returns the category with the wanted id.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <param name="categories">Category repository.</param>
        /// <returns>Return the category with the wanted id.</returns>
        [NonAction]
        private Category GetCategoryById(int? id)
        {
            var categoryById = categories
                .All()
                .Where(x => x.CategoryId == id)
                .FirstOrDefault();

            return categoryById;
        }

        /// <summary>
        /// Get the user with the passed id.
        /// </summary>
        /// <param name="userId">Identifier of the wanted user.</param>
        /// <returns>The user with the passed id</returns>
        [NonAction]
        public User GetUserById(string userId)
        {
            var user = users.All().Where(x => x.Id == userId).FirstOrDefault();

            return user;
        }

        [NonAction]
        private QuestionViewModel CreateNewQuestion(Question newQuestion)
        {
            return new QuestionViewModel
            {
                Title = newQuestion.Title,
                QuestionContent = newQuestion.QuestionContent,
                TimeOfCreation = newQuestion.TimeOfCreation,
                CategoryId = newQuestion.CategoryId,
                Answers = newQuestion.Answers,
                UserId = newQuestion.UserId, // Get the current user id
                User = newQuestion.User
            };
        }

        public ActionResult Index(int? id)
        {
            // If the id is not passed in the TempData - pass it, if so - skip it.
            // If this checking does not exist, the method might try to pass
            // the same id with the same key and that will cause an exception.
            if (!TempData.ContainsKey("CategoryID"))
            {
                TempData.Add("CategoryID", id);
            }

            ViewBag.CurrentUser = GetUserById(User.Identity.GetUserId());
            var categoryById = GetCategoryById(id);

            // Select all the questions in the current directory
            IList<Question> allQuestions = new List<Question>();
            allQuestions = categoryById.Questions.ToList<Question>();

            IList<QuestionViewModel> listWithQuestionsInCategory = new List<QuestionViewModel>();

            for (int i = 0; i < allQuestions.Count; i++)
            {
                // Create view model with question
                var currentCategoryQuestion = CreateNewQuestion(allQuestions[i]);

                listWithQuestionsInCategory.Add(currentCategoryQuestion);

            }

            return View(listWithQuestionsInCategory);
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
            IRepository<Category> categories = new Repository<Category>();

            int categoryId = (int)TempData.Values.ElementAt(0);

            if (ModelState.IsValid)
            {
                var categoryById = GetCategoryById(categoryId);
                string userId = User.Identity.GetUserId();
                Question questionToBeAdded = new Question 
                {
                    Title = newQuestion.Title,
                    QuestionContent = newQuestion.QuestionContent,
                    TimeOfCreation = DateTime.Now,
                    CategoryId = categoryId,
                    Category = categoryById,
                    UserId = userId, // Get the current user
                };

                // Save to database
                categoryById.Questions.Add(questionToBeAdded);

                categories.SaveChanges();
            }

            return RedirectToAction("Index", new { id = categoryId });
        }
    }
}