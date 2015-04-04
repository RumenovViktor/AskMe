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
        private IRepository<Question> questions;

        //Poor man's DI. TODO: Use Ninject instead!!!
        public QuestionController()
            :this(new Repository<Question>())
        { 
        }

        public QuestionController(IRepository<Question> questions)
        {
            this.questions = questions;
        }

        [NonAction]
        public Question GetQuestionById(int? id/*, /*IRepository<Question> questions*/)
        {
            var questionById = questions
                .All()
                .Where(x => x.QuestionId == id)
                .FirstOrDefault();

            return questionById;
        }

        [NonAction]
        private AnswerViewModel CreateNewQuestionViewModel(Answer answer)
        {
            return new AnswerViewModel
            {
                AnswerId = answer.AnswerId,
                Content = answer.Content,
                PostDate = answer.PostDate,
                QuestionId = answer.QuestionId,
                Question = answer.Question,
                UserId = answer.UserId,
                User = answer.User,
                Comments = answer.Comments,
            };
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

            //IRepository<Question> questions = new Repository<Question>();

            var questionById = GetQuestionById(id/*, questions*/);
            ViewBag.CurrentQuestion = questionById;

            IList<Answer> allAnswers = questionById.Answers.ToList<Answer>();
            IList<AnswerViewModel> answersToBePosted = new List<AnswerViewModel>();

            for (int i = 0; i < allAnswers.Count; i++)
            {
                AnswerViewModel answerToBeAdded = CreateNewQuestionViewModel(allAnswers[i]);
                answersToBePosted.Add(answerToBeAdded);
            }

            // Return all the answers sorted.
            return View(answersToBePosted.OrderByDescending(o => o.PostDate).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostAnswer(AnswerViewModel newAnswer) 
        {
            int questionId = (int)TempData.Values.ElementAt(1);

            IRepository<Question> questions = new Repository<Question>();
            var questionById = GetQuestionById(questionId/*, questions*/);
            string userId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                Answer answerToBePosted = new Answer
                {
                    Comments = newAnswer.Comments,
                    Content = newAnswer.Content,
                    PostDate = DateTime.Now,
                    Question = questionById,
                    QuestionId = questionId,
                    UserId = userId,
                };

                questionById.Answers.Add(answerToBePosted);
                questions.SaveChanges();
            }

            return RedirectToAction("Index", new { id = questionId });
        }
    }
}