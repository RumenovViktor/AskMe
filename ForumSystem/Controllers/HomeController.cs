namespace ForumSystem.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections;
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity;

    using ForumSystem.Data.Repositories;
    using ForumSystem.Models;

    public class HomeController : Controller
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

        // GET: Home
        public ActionResult Index()
        { 
            IRepository<Category> categories = new Repository<Category>();
            var allCategories = categories.All().ToList<Category>();

            Session["CurrentUser"] = GetUserById(User.Identity.GetUserId());

            return View(allCategories);
        }
    }
}