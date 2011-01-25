using System;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Helpers;
using _2SQUARE.Models;
using _2SQUARE.Services;
using MvcContrib;
using System.Linq;

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
            var viewModel = ProjectTermPredefinedTermsViewModel.Create(Db, _projectService, id, projectId, CurrentUserId);
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

            var viewModel = ProjectTermPredefinedTermsViewModel.Create(Db, _projectService, id, projectId, CurrentUserId);
            return View(viewModel);            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public ActionResult AddNewTerm(int id /*step id*/, int projectId)
        {
            var projectTerm = new ProjectTerm() { ProjectId = projectId };
            var viewModel = ProjectTermAddNewTermViewModel.Create(Db, id, projectTerm);
            return View(viewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Step Id</param>
        /// <param name="projectTerm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddNewTerm(int id /*step id*/, ProjectTerm projectTerm)
        {
            Validation.Validate(projectTerm, ModelState);

            if (ModelState.IsValid)
            {
                 projectTerm = _projectService.AddTermToProject(projectTerm.ProjectId, projectTerm.SquareTypeId, term: projectTerm.Term,
                                                     definition: projectTerm.Definition, source: projectTerm.Source);
                 Message = "Successfully added term to project";

                return RedirectToAction("Step1", projectTerm.SquareType.Name, new {id=id, projectId=projectTerm.ProjectId});
            }

            var viewModel = ProjectTermAddNewTermViewModel.Create(Db, id, projectTerm);
            return View(viewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="stepId">Only used for redirection information</param>
        /// <returns></returns>
        public ActionResult EditTerm(int id /* project term id */, int stepId)
        {
            var viewModel = ProjectTermEditViewModel.Create(Db, id, stepId);

            if (!_projectService.HasAccess(viewModel.ProjectTerm.ProjectId, CurrentUserId)) { return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "Project(" + viewModel.ProjectTerm.ProjectId + ")"))); }

            return View(viewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="stepId">Only used for redirection information</param>
        /// <param name="definitionId"></param>
        /// <param name="definition"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditTerm(int id /* project term id */, int stepId, int definitionId = -1, string definition = null, string source = null)
        {
            var projectTerm = Db.ProjectTerms.Single(a => a.id == id);

            if (definitionId == -1 && string.IsNullOrEmpty(definition) && string.IsNullOrEmpty(source))
            {
                ModelState.AddModelError("Arguments", "No definition is provided.");
            }

            if (definitionId > 0)
            {
                var def = Db.Definitions.Single(a => a.id == definitionId);
                projectTerm.Definition = def.Description;
                projectTerm.Source = def.Source;
            }
            else
            {
                projectTerm.Definition = definition;
                projectTerm.Source = source;
            }

            if (ModelState.IsValid)
            {
                Db.SaveChanges();
                Message = string.Format("Definition for {0} has been updated.", projectTerm.Term);

                return this.RedirectToAction("Step1", projectTerm.SquareType.Name, new {id=stepId, projectId=projectTerm.ProjectId});

            }

            var viewModel = ProjectTermEditViewModel.Create(Db, id, stepId);
            viewModel.ProjectTerm = projectTerm;
            return View(viewModel);
        }

        /// <summary>
        /// Remove term from step
        /// </summary>
        /// <param name="id">Project Term Id</param>
        /// <param name="stepId">Used for redirection information only</param>
        /// <returns></returns>
        [HttpPost]
        public RedirectToRouteResult RemoveTerm(int id /*project term id*/, int stepId)
        {
            var step = Db.ProjectSteps.Where(a => a.Id == stepId).SingleOrDefault();
            var projectTerm = Db.ProjectTerms.Where(a => a.id == id).SingleOrDefault();
            var term = projectTerm.Term;

            if (step == null || projectTerm == null)
            {
                ErrorMessage = "Unable to find either step or project term.";
                return this.RedirectToAction<ErrorController>(a => a.Index());
            }
            if (projectTerm.ProjectId != step.ProjectId)
            {
                ErrorMessage = "Project mismatch, term project does not match step project.";
                return this.RedirectToAction<ErrorController>(a => a.Index());
            }

            Db.DeleteObject(projectTerm);
            Db.SaveChanges();

            Message = string.Format(Messages.Deleted, term);
            return RedirectToAction(step.Step.Action, step.Step.Controller, new {id=stepId, projectId=step.ProjectId});
        }
    }
}
