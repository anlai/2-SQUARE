using System;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.Models;
using _2SQUARE.Services;
using MvcContrib;

namespace _2SQUARE.Controllers
{
    [Authorize]
    public class SecurityController : SuperController
    {
        private readonly IProjectService _projectService;

        public SecurityController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        #region Step 1
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <returns></returns>
        public ActionResult Step1(int id, int projectId)
        {
            var viewModel = SecurityStep1ViewModel.Create(Db, id, projectId);
            return View(viewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <param name="termId"></param>
        /// <param name="definitionId"></param>
        /// <returns></returns>
        [HttpPost]
        public RedirectToRouteResult AddTerm(int id, int projectId, int squareTypeId, int termId, int definitionId)
        {
            try
            {
                _projectService.AddTermToProject(projectId, squareTypeId, termId: termId, definitionId: definitionId);
                Message = "Successfully added term to project.";
            }
            catch
            {
                Message = "Unable to add term to project";
            }

            return this.RedirectToAction(a => a.Step1(id, projectId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <param name="term"></param>
        /// <param name="definition"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpPost]
        public RedirectToRouteResult AddNewTerm(int id, int projectId, int squareTypeId, string term, string definition, string source)
        {
            try
            {
                _projectService.AddTermToProject(id, squareTypeId, term: term, definition: definition, source: source);
                Message = "Successfully added term to project";
            }
            catch
            {
                Message = "Unable to add term to project.";
            }

            return this.RedirectToAction(a => a.Step1(id, projectId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <returns></returns>
        [HttpPost]
        public RedirectToRouteResult RemoveTerm(int id, int projectId)
        {
            return this.RedirectToAction(a => a.Step1(id, projectId));
        }
        #endregion

        public ActionResult Step2()
        {
            throw new NotImplementedException();

            return View();
        }

        public ActionResult Step3()
        {
            throw new NotImplementedException();

            return View();
        }

        public ActionResult Step4()
        {
            throw new NotImplementedException();

            return View();
        }

        public ActionResult Step5()
        {
            throw new NotImplementedException();

            return View();
        }

        public ActionResult Step6()
        {
            throw new NotImplementedException();

            return View();
        }

        public ActionResult Step7()
        {
            throw new NotImplementedException();

            return View();
        }

        public ActionResult Step8()
        {
            throw new NotImplementedException();

            return View();
        }

        public ActionResult Step9()
        {
            throw new NotImplementedException();

            return View();
        }

    }
}
