using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TinyMceDemo.Models;
using TinyMceDemo.Models.EF;

namespace TinyMceDemo.Controllers
{
    public class HomeController : Controller
    {
        QuestionDBEntities5 context = new QuestionDBEntities5();

        public ActionResult Index()
        {
            var questions = context.Questions.ToList();
            return View(questions);
        }

        public ActionResult Create()
        {
            return View(new QuestionViewModel());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(QuestionViewModel model)
        {
            var question = new Question();
            question.ID = Guid.NewGuid();
            question.Title = model.Title;
            question.Description = model.Description.Replace("</p>", "&nbsp;</p>");
            context.Questions.Add(question);
            
            for (int i = 0; i < model.AnswerBody.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(model.AnswerBody[i]))
                {
                    var obj = new Answer();
                    obj.AnswerBody = model.AnswerBody[i].Replace("</p>", "&nbsp;</p>");
                    obj.ID = Guid.NewGuid();
                    obj.QuestionID = question.ID;
                    obj.IsRight = model.IsRight != null && model.IsRight.Count() > 0 && model.IsRight.Any(x => x == (i + 1));
                    context.Answers.Add(obj);
                }
            }
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(Guid? id)
        {
            if (id.HasValue)
            {
                var question = context.Questions.Find(id.Value);
                var model = new DetailsViewModel();
                model.Title = question.Title;
                model.ID = question.ID;
                model.Description = question.Description;
                model.Answers = context.Answers.ToList().Where(x => x.QuestionID == id.Value).ToList();

                if (question != null)
                    return View(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(Guid? id)
        {
            if (id.HasValue)
            {
                var question = context.Questions.Find(id.Value);
                var model = new DetailsViewModel();
                model.Title = question.Title;
                model.ID = question.ID;
                model.Description = question.Description;
                model.Answers = context.Answers.ToList().Where(x => x.QuestionID == id.Value).ToList();

                if (question != null)
                    return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(QuestionViewModel model)
        {
            var question = new Question();
            question.ID = model.ID;
            question.Title = model.Title;
            question.Description = model.Description.Replace("</p>", "&nbsp;</p>");

            context.Questions.Attach(question);
            context.Entry(question).State = EntityState.Modified;

            var answers = context.Answers.ToList().Where(x => x.QuestionID == model.ID).ToList();
            foreach (var answer in answers)
            {
                context.Answers.Remove(answer);
            }
            for (int i = 0; i < model.AnswerBody.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(model.AnswerBody[i]))
                {
                    var obj = new Answer();
                    obj.AnswerBody = model.AnswerBody[i].Replace("</p>", "&nbsp;</p>");
                    obj.ID = Guid.NewGuid();
                    obj.QuestionID = question.ID;
                    obj.IsRight = model.IsRight != null && model.IsRight.Count() > 0 && model.IsRight.Any(x => x == (i + 1));
                    context.Answers.Add(obj);
                }
            }
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}