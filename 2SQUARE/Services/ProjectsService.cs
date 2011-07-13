using System;
using System.Collections.Generic;
using System.Security;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Core.Domain;
using _2SQUARE.Helpers;
using System.Linq;
using DesignByContract;
using Resources;

namespace _2SQUARE.Services
{
    public class ProjectsService : IProjectService
    {
        #region Access Methods
        
        /// <summary>
        /// Deterrmines if a user has access to a project
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool HasAccess(int id, string login)
        {
            Check.Require(!string.IsNullOrWhiteSpace(login), "login is required.");

            using (var db = new SquareContext())
            {
                return db.ProjectWorkers.Where(a => a.Project.Id == id && a.User.Username.ToLower() == login.ToLower()).Any();
            }
        }

        /// <summary>
        /// not sure how necessary this is, review when looking at step 1
        /// </summary>
        /// <param name="id"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        public List<string> UserRoles(int id, string login)
        {
            Check.Require(!string.IsNullOrEmpty(login), "login is required.");

            using (var db = new SquareContext())
            {
                return db.ProjectWorkers.Where(a => a.Project.Id == id && a.User.Username == login).Select(a => a.Role.Name).ToList();
            }
        }

        /// <summary>
        /// Does the user have the necessary role in the project
        /// </summary>
        /// <param name="id">project id</param>
        /// <param name="login">user login</param>
        /// <param name="project role id">project role id</param>
        /// <returns></returns>
        public bool IsInProjectRole(int id, string login, string roleId)
        {
            using (var db = new SquareContext())
            {
                return db.ProjectWorkers.Where(a => a.Project.Id == id && a.User.Username == login && a.Role.Id == roleId).Any();    
            }
            
        }

        /// <summary>
        /// Returns a list of projects based on user id
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public IList<Project> GetByUser(string login)
        {
            Check.Require(!string.IsNullOrWhiteSpace(login), "login is required.");

            using (var db = new SquareContext())
            {
                var user = db.Users.Where(a => a.Username == login).Single();

                var projects = db.ProjectWorkers.Where(a => a.User.UserId == user.UserId).Select(a => a.Project);

                return projects.ToList();
            }
        }

        /// <summary>
        /// Returns a project if a user has access to the project
        /// </summary>
        /// <param name="id"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        public Project GetProject(int id, string login)
        {
            Check.Require(!string.IsNullOrEmpty(login), "login is required.");

            if (HasAccess(id, login))
            {
                using (var db = new SquareContext())
                {
                    var project = db.Projects.Include("ProjectSteps")
                                             .Include("ProjectSteps.Step")
                                             .Include("ProjectSteps.Step.SquareType")
                                             .Where(a => a.Id == id).Single();
                    return project;    
                }
            }

            throw new SecurityException(string.Format(Messages.NoAccess, "Project(id=" + id + ")"));
        }

        /// <summary>
        /// Gets a project step
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="login"></param>
        /// <returns></returns>
        public ProjectStep GetProjectStep(int id, string login)
        {
            Check.Require(!string.IsNullOrEmpty(login), "login is required.");

            using (var db = new SquareContext())
            {
                var projectStep = db.ProjectSteps.Include("Project")
                                                 .Include("Step").Include("Step.SquareType")
                                                 .Where(a => a.Id == id).Single();

                if (HasAccess(projectStep.Project.Id, login))
                {
                    return projectStep;
                }
            }

            throw new SecurityException(string.Format(Messages.NoAccess, "Project Step(id=" + id + ")"));
        }

        /// <summary>
        /// Gets all the project steps for a given project
        /// </summary>
        /// <remarks>Does not validate permissions</remarks>
        /// <param name="id">Project Id</param>
        /// <param name="squareType">Square Type</param>
        /// <returns></returns>
        public IList<ProjectStep> GetProjectSteps(int id, SquareType squareType)
        {
            using (var db = new SquareContext())
            {
                var query = db.ProjectSteps.Where(a => a.Project.Id == id);

                if (squareType != null)
                {
                    query = query.Where(a => a.Step.SquareType == squareType);
                }

                return query.ToList();
            }
        }
        #endregion

        /// <summary>
        /// Create the project
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        public Project CreateProject(string name, string description, string login)
        {
            using (var db = new SquareContext())
            {
                // load objects
                var user = db.Users.Where(a => a.Username == login).Single();
                var role = db.ProjectRoles.Where(a => a.Id == ProjectRoles.ProjectManager).Single();
                var steps = db.Steps;
                var squareTypes = db.SquareTypes;

                var project = new Project() { Name = name, Description = description };

                // create the worker for current user
                var worker = new ProjectWorker() { Project = project, Role = role, User = user };
                project.ProjectWorkers.Add(worker);

                // fill in all the project steps
                foreach (var squareTypeId in squareTypes.Select(a => a.Id).ToList())
                {
                    foreach (var step in steps.Where(a => a.SquareType.Id == squareTypeId))
                    {
                        var pstep = new ProjectStep() { Project = project, Step = step };
                        project.ProjectSteps.Add(pstep);
                    }
                }

                // save the project
                db.Projects.Add(project);
                db.SaveChanges();

                return project;    
            }
        }

        #region Step 1
        /// <summary>
        /// Add a term to a project
        /// </summary>
        /// <remarks>Does not validate access</remarks>
        /// <param name="id">Project Id</param>
        /// <param name="squareTypeId">Square Type Id</param>
        /// <param name="term"></param>
        /// <param name="definition"></param>
        /// <param name="source"></param>
        /// <param name="termId"></param>
        /// <param name="definitionId"></param>
        public ProjectTerm AddTermToProject(int id, int squareTypeId, string term, string definition, string source, int termId, int definitionId)
        {
            using (var db = new SquareContext())
            {
                var project = db.Projects.Where(a => a.Id == id).Single();
                var squareType = db.SquareTypes.Where(a => a.Id == squareTypeId).Single();

                // update the parameters with the values from the database
                if (termId > 0 && definitionId > 0)
                {
                    var termObj = db.Terms.Where(a => a.Id == termId).Single();
                    var definitionObj = db.Definitions.Where(a => a.Id == definitionId).Single();

                    // make sure def matches term
                    if (definitionObj.Term.Id != termObj.Id) throw new ArgumentException("Term/Definition mismatch.");

                    // set the parameters, so it can be generated
                    term = termObj.Name;
                    definition = definitionObj.Description;
                    source = definitionObj.Source;
                }

                // add the term to the project
                if (!string.IsNullOrWhiteSpace(term) && !string.IsNullOrWhiteSpace(definition) && !string.IsNullOrWhiteSpace(source))
                {
                    // not project or square type, wtf?
                    // shouldn't happen if null, would have thrown exception in .single() above.
                    if (project == null || squareType == null)
                    {
                        throw new ArgumentException("Project or Square Type Id are invalid");
                    }

                    // create the new term
                    var projectTerm = new ProjectTerm();
                    projectTerm.Term = term;
                    projectTerm.Definition = definition;
                    projectTerm.Source = source;
                    projectTerm.Project = project;
                    projectTerm.SquareType = squareType;

                    // add the project term to the db
                    db.ProjectTerms.Add(projectTerm);
                    db.SaveChanges();

                    return projectTerm;
                }    
            }

            return null;
        }

        /// <summary>
        /// Updates a project's temr
        /// </summary>
        /// <param name="id">project term Id</param>
        /// <param name="projectId">Project Id</param>
        /// <param name="term"></param>
        /// <param name="definition"></param>
        /// <param name="source"></param>
        /// <param name="definitionId"></param>
        /// <returns></returns>
        public ProjectTerm UpdateProjectTerm(int id, int projectId, ModelStateDictionary modelState, string term = null, string definition = null, string source = null, int? definitionId = null)
        {
            using (var db = new SquareContext())
            {
                var projectTerm = db.ProjectTerms.Include("Project").Include("SquareType").Where(a => a.Id == id).Single();
                
                if (definitionId.HasValue)
                {
                    // load the definition
                    var def = db.Definitions.Include("Term").Where(a => a.Id == definitionId.Value).Single();

                    if (def.Term.Name != projectTerm.Term)
                    {
                        modelState.AddModelError("", "Definition/Term mismatch.");
                    }
                    else
                    {
                        // update the values
                        projectTerm.Definition = def.Description;
                        projectTerm.Source = def.Source;    
                    }

                }
                else
                {
                    if (string.IsNullOrWhiteSpace(term) || string.IsNullOrWhiteSpace(definition) || string.IsNullOrWhiteSpace(source))
                    {
                        modelState.AddModelError("", "Term/Definition/Source is empty.");
                    }
                    else
                    {
                        projectTerm.Term = term;
                        projectTerm.Definition = definition;
                        projectTerm.Source = source;
                    }
                }

                if (modelState.IsValid)
                {
                    db.SaveChanges();
                }
            }

            return null;
        }

        #endregion

        // **************************************************
        // below this is not validated against the database
        // **************************************************

        public Goal LoadGoal(int id)
        {
            throw new NotImplementedException();
        }

        public Goal SaveGoal(int id, Goal goal)
        {
            throw new NotImplementedException();
        }

        public void DeleteGoal(int id)
        {
            throw new NotImplementedException();
        }

        public Artifact LoadArtifact(int id)
        {
            throw new NotImplementedException();
        }

        public Artifact SaveArtifact(int id, Artifact artifact, string loginId)
        {
            throw new NotImplementedException();
        }

        public void DeleteArtifact(int id)
        {
            throw new NotImplementedException();
        }

        public void SetAssessmentType(int id, AssessmentType assessmentType, string userId)
        {
            throw new NotImplementedException();
        }

        public RiskRecommendation SaveRiskRecommendation(RiskRecommendation riskRecommendation, int riskId)
        {
            throw new NotImplementedException();
        }

        public void SetElicitationType(int id, ElicitationType elicitationType, string rationale, string userId)
        {
            throw new NotImplementedException();
        }

        public void SaveRequirement(int id, SquareType squareType, Requirement requirement, ModelStateDictionary modelState)
        {
            throw new NotImplementedException();
        }

        public void DeleteRequirement(int id, int requirementId)
        {
            throw new NotImplementedException();
        }


        #region Project Status
        public ProjectStepStatus GetStepStatus(int id, ProjectStep projectStep)
        {
            using (var db = new SquareContext())
            {
                var step = projectStep ?? db.ProjectSteps.Where(a => a.Id == id).SingleOrDefault();

                Check.Require(step != null, "step is required.");

                // hasn't started working yet
                if (!step.DateStarted.HasValue) return ProjectStepStatus.Pending;

                // step is complete
                if (step.Complete) return ProjectStepStatus.Complete;

                // step is working
                return ProjectStepStatus.Working;    
            }
            
        }

        public bool IsStepWorking(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsStepPending(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsStepComplete(int id)
        {
            throw new NotImplementedException();
        }

        public bool CanStepChangeStatus(int id, ProjectStep projectStep)
        {
            throw new NotImplementedException();
        }

        public ProjectStep UpdateStatus(int id, ProjectStepStatus projectStepStatus, string login)
        {
            using (var db = new SquareContext())
            {
                var step = db.ProjectSteps.Include("Project").Include("Step").Where(a => a.Id == id).Single();

                // validate that the step can be changed
                switch (projectStepStatus)
                {
                    case ProjectStepStatus.Pending:
                        step.DateStarted = null;
                        step.Complete = false;
                        break;
                    case ProjectStepStatus.Working:
                        step.DateStarted = DateTime.Now;
                        step.Complete = false;
                        break;
                    case ProjectStepStatus.Complete:
                        step.DateStarted = step.DateStarted.HasValue ? step.DateStarted : DateTime.Now;
                        step.DateCompleted = DateTime.Now;
                        step.Complete = true;
                        break;
                }

                db.SaveChanges();

                return step;
            }
        }
        #endregion
    }
}