namespace ForumSystem.Controllers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity;

    using ForumSystem.Models;
    using ForumSystem.Data.Repositories;

    public class QuestionController : Controller
    {
        private IRepository<User> users = new Repository<User>();

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

        public ActionResult Index(int? id)
        {
            // Save the id so that we can pass it to other actions.
            if (!TempData.ContainsKey("QuestionId"))
            {
                TempData.Add("QuestionId", id);
            }
            else
            {
                TempData["QuestionId"] = id;
            }

            IRepository<Question> questions = new Repository<Question>();

            // Get current user data.
            ViewBag.CurrentUser = GetUserById(User.Identity.GetUserId());
            var questionById = questions.All().Where(x => x.QuestionId == id).FirstOrDefault();

            IList<Answer> allAnswers = new List<Answer>();

            for (int i = 0; i < questionById.Answers.Count; i++)
            {
                // TODO: Add all answers to the list so it can be sent to the view.
            }

            return View();
        }
    }
}