namespace ForumSystem.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections;
    using System.Collections.Generic;

    using ForumSystem.Data.Repositories;
    using ForumSystem.Models;

    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        { 
            IRepository<Category> categories = new Repository<Category>();
            var allCategories = categories.All().ToList<Category>();

            return View(allCategories);
        }
    }
}