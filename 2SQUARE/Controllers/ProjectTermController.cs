using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Core.Domain;
using _2SQUARE.Models;
using _2SQUARE.Services;
using MvcContrib;
using System.Linq;

namespace _2SQUARE.Controllers
{
    public class ProjectTermController : ApplicationController
    {
        private readonly IProjectService _projectService;

        public ProjectTermController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// View Terms that are in the db
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <returns></returns>
        public ActionResult ViewPredefinedTerms(int id /*step id*/, int projectId)
        {
            var viewModel = ProjectTermPredefinedTermsViewModel.Create(Db, _projectService, id, projectId, CurrentUserId);
            return View(viewModel);
        }

        /// <summary>
        /// View Terms that are in the db
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <param name="termId">Id of the term</param>
        /// <param name="definitionId">Id of predefined definition</param>
        /// <param name="definition">New definition for term</param>
        /// <param name="source">New source for term</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ViewPredefinedTerms(int id, int projectId, int termId, int? definitionId, string source, string definition)
        {
            var squareType = Db.ProjectSteps.Where(a => a.Id == id).Select(a => a.Step.SquareType).Single();

            // predefined definition selected
            if (definitionId.HasValue)
            {
                _projectService.AddTermToProject(projectId, squareType.Id, termId: termId, definitionId: definitionId.Value);
                Message = "Successfully added term to project.";    
            }
            // adding a new definition
            else if (!string.IsNullOrWhiteSpace(source) && !string.IsNullOrWhiteSpace(definition))
            {
                var term = Db.Terms.Where(a => a.Id == termId).Single();
                _projectService.AddTermToProject(projectId, squareType.Id, term: term.Name, definition: definition, source: source);
                Message = "Successfully added term to project.";    
            }

            var viewModel = ProjectTermPredefinedTermsViewModel.Create(Db, _projectService, id, projectId, CurrentUserId);
            return View(viewModel);            
        }

        /// <summary>
        /// Add a brand new term (not necessarily in the db)
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public ActionResult AddNewTerm(int id /*step id*/, int projectId)
        {
            var project = Db.Projects.Where(a => a.Id == projectId).Single();
            var projectStep = Db.ProjectSteps.Include("Step").Include("Step.SquareType").Where(a => a.Id == id).Single();

            var projectTerm = new ProjectTerm() { Project = project, SquareType = projectStep.Step.SquareType};

            var viewModel = ProjectTermAddNewTermViewModel.Create(Db, id, projectTerm);
            return View(viewModel);
        }

        /// <summary>
        /// Add a brand new term (not necessarily in the db)
        /// </summary>
        /// <param name="id">Step Id</param>
        /// <param name="projectTerm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddNewTerm(int id, int projectId, ProjectTerm projectTerm)
        {
            // remove out errors that shouldn't be there, project and square type
            ModelState.Remove("projectTerm.Project");
            ModelState.Remove("projectTerm.Squaretype");

            var project = Db.Projects.Where(a => a.Id == projectId).Single();
            var projectStep = Db.ProjectSteps.Include("Step").Include("Step.SquareType").Where(a => a.Id == id).Single();

            if (ModelState.IsValid)
            {
                 projectTerm = _projectService.AddTermToProject(project.Id, projectStep.Step.SquareType.Id
                                                            , term: projectTerm.Term
                                                            , definition: projectTerm.Definition
                                                            , source: projectTerm.Source);
                 
                Message = "Successfully added term to project";

                return RedirectToAction("Step1", projectTerm.SquareType.Name, new {id=id, projectId=projectId});
            }

            projectTerm.Project = project;
            var viewModel = ProjectTermAddNewTermViewModel.Create(Db, id, projectTerm);
            return View(viewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Project Term id</param>
        /// <param name="projectStepId">Only used for redirection information</param>
        /// <returns></returns>
        public ActionResult EditTerm(int id, int projectStepId)
        {
            var viewModel = ProjectTermEditViewModel.Create(Db, id, projectStepId);

            if (!_projectService.HasAccess(viewModel.ProjectTerm.Project.Id, CurrentUserId))
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "Project(" + viewModel.ProjectTerm.Project.Id + ")")));
            }

            return View(viewModel);
        }

        /// <summary>
        /// Edit a project term
        /// </summary>
        /// <param name="id">Project Term Id</param>
        /// <param name="projectStepId">Only used for redirection information</param>
        /// <param name="definitionId"></param>
        /// <param name="definition"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditTerm(int id, int projectStepId, int? definitionId = null, string definition = null, string source = null)
        {
            var projectStep = Db.ProjectSteps.Include("Project").Where(a => a.Id == projectStepId).Single();
            var projectTerm = Db.ProjectTerms.Include("SquareType").Where(a => a.Id == id).Single();

            ModelState.Clear();

            _projectService.UpdateProjectTerm(id, projectStep.Project.Id, ModelState, projectTerm.Term, definition, source, definitionId);

            if (ModelState.IsValid)
            {
                Message = "Project term updated";
                return this.RedirectToAction("Step1", projectTerm.SquareType.Name, new { id = projectStepId, projectId = projectTerm.Project.Id });
            }

            var viewModel = ProjectTermEditViewModel.Create(Db, id, projectStepId);
            viewModel.ProjectTerm = projectTerm;
            return View(viewModel);
        }

        /// <summary>
        /// Remove term from step
        /// </summary>
        /// <param name="id">Project Term Id</param>
        /// <param name="project step id">Used for redirection information only</param>
        /// <returns></returns>
        [HttpPost]
        public RedirectToRouteResult RemoveTerm(int id, int projectStepId)
        {
            var step = Db.ProjectSteps.Where(a => a.Id == projectStepId).SingleOrDefault();
            var projectTerm = Db.ProjectTerms.Where(a => a.Id == id).SingleOrDefault();
            var term = projectTerm.Term;

            if (step == null || projectTerm == null)
            {
                ErrorMessage = "Unable to find either step or project term.";
                return this.RedirectToAction<ErrorController>(a => a.Index());
            }
            if (projectTerm.Project.Id != step.Project.Id)
            {
                ErrorMessage = "Project mismatch, term project does not match step project.";
                return this.RedirectToAction<ErrorController>(a => a.Index());
            }

            Db.ProjectTerms.Remove(projectTerm);
            Db.SaveChanges();

            Message = string.Format(Messages.Deleted, term);
            return RedirectToAction(step.Step.Action, step.Step.Controller, new { id = projectStepId, projectId = step.Project.Id });
        }
    }
}
