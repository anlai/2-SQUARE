using System;
using System.Web.Mvc;
using _2SQUARE.Helpers;
using _2SQUARE.Models;
using _2SQUARE.Services;
using MvcContrib;

namespace _2SQUARE.Controllers
{
    public class ProjectTermController : SuperController
    {
        private readonly IProjectService _projectService;

        public ProjectTermController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public ActionResult ViewPredefinedTerms(int id /*step id*/, int projectId)
        {
            var viewModel = SecurityStep1PredefinedTermsViewModel.Create(Db, _projectService, id, projectId, CurrentUserId);
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
        public ActionResult ViewPredefinedTerms(int id /*step id*/, int projectId, int squareTypeId, int termId, int definitionId)
        {
            _projectService.AddTermToProject(projectId, squareTypeId, termId: termId, definitionId: definitionId);
            Message = "Successfully added term to project.";

            var viewModel = SecurityStep1PredefinedTermsViewModel.Create(Db, _projectService, id, projectId, CurrentUserId);
            return View(viewModel);            
        }

        public ActionResult AddNewTerm(int id /*step id*/, int projectId)
        {
            var projectTerm = new ProjectTerm() { ProjectId = projectId };
            var viewModel = SecurityStep1AddNewTermViewModel.Create(Db, id, projectTerm);
            return View(viewModel);
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
        public ActionResult AddNewTerm(int id /*step id*/, ProjectTerm projectTerm)
        {
            Validation.Validate(projectTerm, ModelState);

            if (ModelState.IsValid)
            {
                 _projectService.AddTermToProject(id, projectTerm.SquareTypeId, term: projectTerm.Term,
                                                     definition: projectTerm.Definition, source: projectTerm.Source);
                    Message = "Successfully added term to project";

                return RedirectToAction("Step1", projectTerm.SquareType.Name);
            }

            var viewModel = SecurityStep1AddNewTermViewModel.Create(Db, id, projectTerm);
            return View(viewModel);
        }
    }
}
