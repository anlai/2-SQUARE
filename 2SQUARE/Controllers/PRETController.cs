using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Core.Domain;
using _2SQUARE.Models;
using _2SQUARE.Services;
using MvcContrib;

namespace _2SQUARE.Controllers
{
    /// <summary>
    /// This is a controller for implementing a version of the computer aided
    /// Privacy Requirement Elicitation Technique as described in
    /// "Computer-Aided Privacy Requirements Elicitation Technique"
    /// by Seiya Miyazaki, Nancy Mead and Justin Zhan
    /// at the 2008 IEEE Asian-Pacific Services Computing Conference
    /// </summary>
    public class PRETController : ApplicationController, IProcedureController
    {
        private readonly IProjectService _projectService;

        public PRETController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public ActionResult Index(int id, int projectId)
        {
            try
            {
                var viewModel = PRETViewModel.Create(Db, _projectService, projectId, id, CurrentUserId);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        /// <summary>
        /// Presents the questions to the user to ask the questions
        /// to figure out which laws apply to the project
        /// </summary>
        /// <param name="id"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public ActionResult Run(int id, int projectId)
        {
            try
            {
                var viewModel = PRETViewModel.Create(Db, _projectService, projectId, id, CurrentUserId, true);
                return View(viewModel);
            }
            catch (Exception)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        [HttpPost]
        public ActionResult Run(int id, int projectId, List<PRETQuestionAnswer> pretQuestionAnswers)
        {
            // all questions must be answered
            if (pretQuestionAnswers.Any(a => a.AnswerId == 0 && !a.IsSubQuestion))
            {
                ModelState.AddModelError("Question", "At least one question was not answered.");
            }

            if (ModelState.IsValid)
            {
                // figure out which laws and redirect to the result page
                var applicableLaws = DetermineLaws(pretQuestionAnswers);

                TempData["applicableLaws"] = applicableLaws;
                // redirect to the results
                return this.RedirectToAction(a => a.Result(id, projectId));
            }

            var viewModel = PRETViewModel.Create(Db, _projectService, projectId, id, CurrentUserId, true, pretQuestionAnswers);
            return View(viewModel);
        }

        /// <summary>
        /// Displays out the results of the questionaire
        /// </summary>
        /// <param name="id"></param>
        /// <param name="projectId"></param>
        /// <param name="laws"></param>
        /// <returns></returns>
        public ActionResult Result(int id,  int projectId)
        {
            try
            {
                var laws = (List<int>) TempData["applicableLaws"];

                var viewModel = PRETResultViewModel.Create(Db, _projectService, projectId, id, CurrentUserId, laws.ToArray());
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));               
            }
        }

        [HttpPost]
        public ActionResult Result(int id, int projectId, int[] lawIds)
        {
            try
            {
                // load all the laws
                var requirements = Db.PretRequirements.Where(a => lawIds.Contains(a.Law.Id)).ToList();
                var project = Db.Projects.Where(a => a.Id == projectId).FirstOrDefault();
                var reqs = requirements.Select(a => new Requirement()
                                     {
                                         Project = project,
                                         Name = a.Name,
                                         //Requirement1 = a.Requirement,
                                         //RequirementId = string.Format("{0}-{1}", a.PRETLaw.id, a.id),
                                         //SquareType = Db.SquareTypes.Where(b=>b.Name ==SquareTypes.Privacy).Select(b=>b.id).First()
                                     }).ToList();

                foreach(var a in reqs) Db.Requirements.Add(a);

                Db.SaveChanges();

                Message = string.Format("{0} requirements have been added to the project.", reqs.Count);
                return this.RedirectToAction(a => a.Index(id, projectId));
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));               
            }

            return View();
        }

        private List<int> DetermineLaws(List<PRETQuestionAnswer> pretQuestionAnswers)
        {
            var applicableLaws = new List<int>();
            //var currentLawId = 0;

            //// get all the laws
            //var laws = Db.PRETLaws.ToList();

            //// see of we can match the answers for any of the laws
            //foreach (var law in laws)
            //{
            //    // set the current law we are looking at
            //    currentLawId = law.id;

            //    // get the distinct questions that apply to this law
            //    //  some laws may not apply to a specific question
            //    // and some questions may have multiple answers that apply to this law
            //    // this will only go through questions that have answers that apply to the law, those that don't are ignored
            //    foreach (var question in law.PRETAnswerXLaws.Select(a => a.PRETAnswer.PRETQuestion).Distinct())
            //    {
            //        // get the answers that apply to the law from this question
            //        var answers = law.PRETAnswerXLaws.Where(a => a.PRETAnswer.PRETQuestion == question)
            //                                         .Select(a => a.PRETAnswer);

            //        // get the answer from the user's answer
            //        var userAnswer = pretQuestionAnswers.Where(a => a.QuestionId == question.Id).FirstOrDefault();

            //        if (!answers.Any(a => a.Id == userAnswer.AnswerId))
            //        {
            //            // not a valid law
            //            currentLawId = -1;

            //            // exit the loop we are dont
            //            break;
            //        }
            //    }

            //    if (currentLawId > 0)
            //    {
            //        applicableLaws.Add(currentLawId);
            //    }
            //}

            return applicableLaws;
        }
    }
}
