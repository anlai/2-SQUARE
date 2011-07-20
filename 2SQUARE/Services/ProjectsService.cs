using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
                                             .Include("Goals").Include("Goals.GoalType")
                                             .Include("SecurityAssessmentType")
                                             .Include("PrivacyAssessmentType")
                                             .Include("SecurityElicitationType")
                                             .Include("PrivacyElicitationType")
                                             .Include("Requirements")
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

        #region Step 2
        /// <summary>
        /// Loads a goal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Goal LoadGoal(int id)
        {
            using (var db = new SquareContext())
            {
                return db.Goals.Include("GoalType").Where(a => a.Id == id).SingleOrDefault();
            }
        }

        /// <summary>
        /// Save a goal
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="goal">Goal (Description and GoalType should be populated)</param>
        /// <param name="goalId">Goal Id for exisitng</param>
        /// <returns></returns>
        public Goal SaveGoal(int id, Goal goal, int? goalId = null, string goalTypeId = null)
        {
            using (var db = new SquareContext())
            {
                // load the project step
                var projectStep = db.ProjectSteps
                                    .Include("Step").Include("Step.SquareType")
                                    .Include("Project")
                                    .Where(a => a.Id == id).Single();

                // list of goal types for this square type
                var goalTypes = db.GoalTypes.Where(a => a.SquareType.Id == projectStep.Step.SquareType.Id).Select(a => a.Id).ToList();

                var goalType = goal.GoalType ?? db.GoalTypes.Where(a => a.Id == goalTypeId).Single();

                // wrong goal type for the project step
                if (!goalTypes.Contains(goalType.Id) && (goalType.Id != GoalTypes.Business)) return null;

                // updating an existing goal
                if (goalId.HasValue)
                {
                    var goalToSave = db.Goals.Include("SquareType").Include("Project").Include("GoalType")
                                   .Where(a => a.Id == goalId.Value).Single();

                    goalToSave.Description = goal.Description;
                    goalToSave.GoalType = goalType;
                    goal = goalToSave;
                }
                else
                {
                    goal.Description = goal.Description;
                    goal.SquareType = projectStep.Step.SquareType;
                    goal.Project = projectStep.Project;
                    goal.GoalType = goalType;

                    db.Goals.Add(goal);
                }
                

                db.SaveChanges();

                return goal;    
            }
        }

        public void DeleteGoal(int id)
        {
            using (var db = new SquareContext())
            {
                var goal = db.Goals.Where(a => a.Id == id).SingleOrDefault();

                if (goal != null)
                {
                    db.Goals.Remove(goal);
                    db.SaveChanges();
                }
            }
        }
        #endregion

        #region Step 3

        /// <summary>
        /// Loads an artifact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Artifact LoadArtifact(int id)
        {
            using (var db = new SquareContext())
            {
                return db.Artifacts.Include("ArtifactType").Include("Project").Where(a => a.Id == id).SingleOrDefault();
            }
        }

        /// <summary>
        /// Save an artifact
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="artifact"></param>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public Artifact SaveArtifact(int id, Artifact artifact, int? artifactId, int? artifactTypeId)
        {
            using (var db = new SquareContext())
            {
                // load the project step
                var projectStep = db.ProjectSteps
                                    .Include("Step").Include("Step.SquareType")
                                    .Include("Project")
                                    .Where(a => a.Id == id).Single();

                // list of artifact types for this square type
                var artifactTypes = db.ArtifactTypes.Where(a => a.SquareType.Id == projectStep.Step.SquareType.Id).Select(a => a.Id).ToList();

                var artifactType = artifact.ArtifactType ?? db.ArtifactTypes.Include("SquareType").Where(a => a.Id == artifactTypeId).Single();

                // wrong artifact type for the project step
                if (!artifactTypes.Contains(artifactType.Id)) return null;

                // update an existing artifact
                if (artifactId.HasValue)
                {
                    var artifactToSave = db.Artifacts.Where(a => a.Id == artifactId).Single();

                    artifactToSave.Name = artifact.Name;
                    artifactToSave.Description = artifact.Description;
                    artifactToSave.ArtifactType = artifactType;

                    // only update file contents if there is a new file, otherwise keep old contents
                    if (artifact.ContentType != null && artifact.Data != null)
                    {
                        artifactToSave.ContentType = artifact.ContentType;
                        artifactToSave.Data = artifact.Data;    
                    }
                }
                // fill in the new artifact
                else
                {
                    artifact.ArtifactType = artifactType;
                    artifact.Project = projectStep.Project;

                    db.Artifacts.Add(artifact);
                }

                db.SaveChanges();

                return artifact;
            }
        }

        /// <summary>
        /// Delete artifact
        /// </summary>
        /// <param name="id"></param>
        public void DeleteArtifact(int id)
        {
            using (var db = new SquareContext())
            {
                var artifact = db.Artifacts.Where(a => a.Id == id).SingleOrDefault();

                if (artifact != null)
                {
                    db.Artifacts.Remove(artifact);
                    db.SaveChanges();
                }
            }
        }
        
        #endregion

        #region Step 4

        /// <summary>
        /// Set the assessment type on the project
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <param name="assessmentType">Assessment Type</param>
        /// <param name="userId">User Login</param>
        public void SetAssessmentType(int id, int assessmentTypeId, string userId)
        {

            using (var db = new SquareContext())
            {

                var project = db.Projects.Where(a => a.Id == id).Single();
                var assessmentType = db.AssessmentTypes.Include("SquareType").Where(a => a.Id == assessmentTypeId).Single();

                if (assessmentType.SquareType.Name == SquareTypes.Security)
                {
                    project.SecurityAssessmentType = assessmentType;
                }
                else if (assessmentType.SquareType.Name == SquareTypes.Privacy)
                {
                    project.PrivacyAssessmentType = assessmentType;
                }
                else
                {
                    // incorrect assessment type
                    throw new Exception("Something funny with the assessment type.");
                }

                db.SaveChanges();

            }

        }

        #endregion

        #region Step 5

        /// <summary>
        /// Set the requirements elicitation type
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <param name="elicitationTypeId">Elicitation Type Id</param>
        /// <param name="rationale">Rationale for selecting elicitation type</param>
        /// <param name="userId">User login id</param>
        public void SetElicitationType(int id, int elicitationTypeId, string rationale, string userId)
        {
            if (!HasAccess(id, userId)) throw new SecurityException("Not authorzied for project.");

            using (var db = new SquareContext())
            {

                // load the objects
                var project = db.Projects.Where(a => a.Id == id).Single();
                var elicitationType = db.ElicitationTypes.Include("SquareType").Where(a => a.Id == elicitationTypeId).Single();

                // set the elicitation type
                if (elicitationType.SquareType.Name == SquareTypes.Security)
                {
                    project.SecurityElicitationType = elicitationType;
                    project.SecurityElicitationRationale = rationale;
                }
                else
                {
                    project.PrivacyElicitationType = elicitationType;
                    project.PrivacyElicitationRationale = rationale;
                }

                db.SaveChanges();
            }
        }

        #endregion

        #region Step 6

        /// <summary>
        /// Save a Requirement
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <param name="squareType">Square Type Id</param>
        /// <param name="requirement"></param>
        /// <param name="modelState"></param>
        public void SaveRequirement(int id, int squareTypeId, Requirement requirement, int? requirementId = null)
        {
            using (var db = new SquareContext())
            {
                var project = db.Projects.Where(a => a.Id == id).Single();
                var squareType = db.SquareTypes.Where(a => a.Id == squareTypeId).Single();

                // adding a new one
                if (!requirementId.HasValue)
                {
                    requirement.Project = project;
                    requirement.SquareType = squareType;
                    
                    db.Requirements.Add(requirement);
                }
                // updating an existing one
                else
                {
                    // load the existing one
                    var existingReq = db.Requirements.Where(a => a.Id == requirementId.Value).Single();

                    // update the values
                    existingReq.RequirementId = requirement.RequirementId;
                    existingReq.Name = requirement.Name;
                    existingReq.RequirementText = requirement.RequirementText;

                    existingReq.Project = project;
                    existingReq.SquareType = squareType;
                }

                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    
                    throw;
                }
                
            }

        }

        /// <summary>
        /// Delete the requirement
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <param name="requirementId">Requirement Id</param>
        /// <param name="login">Login Id</param>
        public void DeleteRequirement(int id, int requirementId, string login)
        {
            var project = GetProject(id, login);

            using (var db = new SquareContext())
            {
                
                // load the requirement
                var requirement = db.Requirements.Where(a => a.Id == requirementId && a.Project.Id == project.Id).Single();

                // delete the requirement
                db.Requirements.Remove(requirement);

                // save
                db.SaveChanges();
            }
        }

        #endregion

        #region Step 7 Methods
        public void SaveCategory(int id, Category category, int? categoryId)
        {
            using (var db = new SquareContext())
            {
                var projectStep = db.ProjectSteps.Include("Project")
                                                 .Include("Step")
                                                 .Include("Step.SquareType")
                                                 .Where(a => a.Id == id).Single();

                // update existing category
                if (categoryId.HasValue)
                {
                    var categoryToSave = db.Categories.Where(a => a.Id == categoryId).Single();

                    categoryToSave.Name = category.Name;
                }
                // setting an existing one
                else
                {
                    category.Project = projectStep.Project;
                    category.SquareType = projectStep.Step.SquareType;

                    db.Categories.Add(category);
                }

                db.SaveChanges();
            }
        }

        public void DeleteCategory(int id, int categoryId, string loginId)
        {
            var project = GetProject(id, loginId);

            using (var db = new SquareContext())
            {

                var category = db.Categories.Where(a => a.Id == categoryId).Single();

                db.Categories.Remove(category);
                db.SaveChanges();
            }
        }

        public void CategorizeRequirement(int id, int categoryId, int requirementId, bool essential, string loginId)
        {
            var project = GetProject(id, loginId);

            using (var db = new SquareContext())
            {
                // load objects
                var category = db.Categories.Where(a => a.Id == categoryId).Single();
                var requirement = db.Requirements.Include("Project").Include("SquareType").Where(a => a.Id == requirementId).Single();

                requirement.Category = category;
                requirement.Essential = essential;

                db.SaveChanges();                
            }
        }

        #endregion

        #region Step 8
        public void UpdateRequirementOrder(int id, int squareTypeId, int[] requirementIds, string loginId)
        {
            var project = GetProject(id, loginId);

            using (var db = new SquareContext())
            {

                for (int i = 0; i < requirementIds.Length; i++ )
                {
                    var reqId = requirementIds.ElementAt(i);
                    // load the requirement
                    var req = db.Requirements.Include("Project").Include("SquareType").Where(a => a.Id == reqId).Single();

                    // validate that we have a valid project and square type for this requirment
                    if (req.Project.Id != id && req.SquareType.Id != req.SquareType.Id)
                    {
                        throw new Exception("Invalid requirement for project and square type.");
                    }

                    // update the order field
                    req.Order = i;

                }

                db.SaveChanges();
            }
        }

        public void UpdateRequirementPriority(int id, int? priority, string loginId)
        {
            using (var db = new SquareContext())
            {
                var requirement = db.Requirements.Include("Project").Include("SquareType").Where(a => a.Id == id).Single();

                requirement.Priority = priority;

                db.SaveChanges();
            }
        }
        #endregion







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
            return true;
        }

        public bool IsStepPending(int id)
        {
            return true;
        }

        public bool IsStepComplete(int id)
        {
            return true;
        }

        public bool CanStepChangeStatus(int id, ProjectStep projectStep)
        {
            return true;
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