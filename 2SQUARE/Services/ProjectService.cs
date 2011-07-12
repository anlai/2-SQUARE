﻿using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Security;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Core.Domain;
using _2SQUARE.Helpers;
using _2SQUARE.Models;
using DesignByContract;

namespace _2SQUARE.Services
{
    public class ProjectService : IProjectService
    {
        SquareContext db = new SquareContext();

        #region Access Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool HasAccess(int id, string login)
        {
            Check.Require(!string.IsNullOrEmpty(login), "login is required.");

            return db.ProjectWorkers.Where(a => a.Project.Id == id && a.User.Username.ToLower() == login.ToLower()).Any();
        }

        public List<string> UserRoles(int id, string login)
        {
            Check.Require(!string.IsNullOrEmpty(login), "login is required.");

            return
                db.ProjectWorkers.Where(a => a.Project.Id == id && a.User.Username == login).Select(a => a.Role.Name).ToList();
        }

        public bool IsInProjectRole(int id /* project id */,string login, string roleName)
        {
            return
                db.ProjectWorkers.Where(a => a.Project.Id == id && a.User.Username == login && a.Role.Name == roleName)
                .Any();
        }

        /// <summary>
        /// Returns a list of projects based on user id
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public IList<Project> GetByUser(string login)
        {
            Check.Require(!string.IsNullOrEmpty(login), "login is required.");

            var user = db.Users.Where(a => a.Username == login).Single();

            return user.ProjectWorkers.Select(a=>a.Project).ToList();
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

            var user = db.Users.Where(a => a.Username == login).Single();
            var project = db.Projects.Where(a => a.Id == id).Single();

            if (!project.ProjectWorkers.Any(a => a.User.UserId == user.UserId)) throw new SecurityException(string.Format(Messages.NoAccess, "Project(id="+id+")"));

            return project;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="login"></param>
        /// <returns></returns>
        public ProjectStep GetProjectStep(int id, string login)
        {
            Check.Require(!string.IsNullOrEmpty(login) , "login is required.");

            var user = db.Users.Where(a => a.Username == login).Single();
            var projectStep = db.ProjectSteps.Where(a => a.Id == id).Single();

            if (!projectStep.Project.ProjectWorkers.Any(a=>a.User.UserId == user.UserId))
                throw new SecurityException(string.Format(Messages.NoAccess, "Project(id="+id+")"));

            return projectStep;
        }
        public IList<ProjectStep> GetProjectSteps(int id, SquareType squareType = null)
        {
            var query = db.ProjectSteps.Where(a => a.Project.Id == id);

            if (squareType != null)
            {
                query = query.Where(a => a.Step.SquareType.Id == squareType.Id);
            }

            return query.ToList();
        }
        #endregion

        #region Step 1 Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <param name="term"></param>
        /// <param name="definition"></param>
        /// <param name="source"></param>
        /// <param name="termId"></param>
        /// <param name="definitionId"></param>
        public ProjectTerm AddTermToProject(int id, int SquareType, string term = null, string definition = null, string source = null, int termId = 0, int definitionId = 0)
        {
            var project = db.Projects.Where(a => a.Id == id).FirstOrDefault();
            var squareType = db.SquareTypes.Where(a => a.Id == SquareType).FirstOrDefault();

            // update existing project term))
            if (termId > 0 && definitionId > 0)
            {
                var termObj = db.Terms.Where(a => a.Id == termId).Single();
                var definitionObj = db.Definitions.Where(a => a.Id == definitionId).Single();

                // make sure def matches term
                if (definitionObj.Term.Id != termObj.Id) throw new ArgumentException("Term/Definition mismatch.");

                term = termObj.Name;
                definition = definitionObj.Description;
                source = definitionObj.Source;
            }

            if (!string.IsNullOrEmpty(term) && !string.IsNullOrEmpty(definition) && !string.IsNullOrEmpty(source))
            {
                if (id <= 0 || SquareType <= 0) throw new ArgumentException("Project or Square Type Id are invalid");

                var projectTerm = new ProjectTerm();
                projectTerm.Term = term;
                projectTerm.Definition = definition;
                projectTerm.Source = source;
                projectTerm.Project = project;
                projectTerm.SquareType = squareType;

                db.ProjectTerms.Add(projectTerm);

                db.SaveChanges();

                return projectTerm;
            }

            return null;
        }
        #endregion

        #region Step 2 Methods
        public Goal LoadGoal(int id)
        {
            return db.Goals.Where(a => a.Id == id).SingleOrDefault();
        }
        /// <summary>
        /// Add/Update goal
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="goal"></param>
        /// <returns></returns>
        public Goal SaveGoal(int id /* projectStep Id */, Goal goal)
        {
            // load the project step
            var projectStep = db.ProjectSteps.Where(a => a.Id == id).Single();
            // list of goal types for this square type
            var goalTypes = db.GoalTypes.Where(a=>a.SquareType == projectStep.Step.SquareType).Select(a=>a.Id).ToList();

            // wrong goal type for the project step
            if (!goalTypes.Contains(goal.GoalType.Id)) return null;

            // create new
            if (goal.Id <= 0)
            {
                goal.Project = projectStep.Project;
                db.Goals.Add(goal);    
            }

            db.SaveChanges();

            return goal;
        }

        public void DeleteGoal(int id /* goal id */)
        {
            var goal = db.Goals.Where(a => a.Id == id).Single();
            db.Goals.Remove(goal);
            db.SaveChanges();
        }
        #endregion

        #region Step 3 Methods
        public Artifact LoadArtifact(int id)
        {
            return db.Artifacts.Where(a => a.Id == id).SingleOrDefault();
        }

        public Artifact SaveArtifact(int id, Artifact artifact, string loginId)
        {
            var projectStep = db.ProjectSteps.Where(a => a.Id == id).Single();
            var artifactType = db.ArtifactTypes.Where(a => a.Id == artifact.ArtifactType.Id).Single();

            // incorrect square type
            if (projectStep.Step.SquareType != artifactType.SquareType) return null;

            artifact.ArtifactType = artifactType;

            if (artifact.Id <= 0)
            {
                artifact.DateCreated = DateTime.Now;
                artifact.CreatedBy = loginId;
                artifact.Project = projectStep.Project;

                db.Artifacts.Add(artifact);
            }

            db.SaveChanges();

            return artifact;
        }

        public void DeleteArtifact(int id)
        {
            var artifact = db.Artifacts.Where(a => a.Id == id).Single();
            db.Artifacts.Remove(artifact);
            db.SaveChanges();
        }
        #endregion

        #region Step 4 Methods
        public void SetAssessmentType(int id /* project id */, AssessmentType assessmentType, string userId)
        {
            var project = GetProject(id, userId);

            Check.Require(project != null, "project is required.");
            Check.Require(assessmentType != null, "assessmentType is required.");

            if (assessmentType.SquareType.Name == SquareTypes.Security)
            {
                project.SecurityAssessmentType = assessmentType;
            }
            else if (assessmentType.SquareType.Name == SquareTypes.Privacy)
            {
                project.PrivacyAssessmentType = assessmentType;
            }

            // save
            db.SaveChanges();
        }

        public RiskRecommendation SaveRiskRecommendation(RiskRecommendation riskRecommendation, int riskId)
        {
            riskRecommendation.Risk.Id = riskId;

            if (riskRecommendation.Id <= 0)
            {
                db.RiskRecommendations.Add(riskRecommendation);
            }

            db.SaveChanges();

            return riskRecommendation;
        }
        #endregion

        #region Step 5 Methods
        public void SetElicitationType(int id, ElicitationType elicitationType, string rationale, string userId)
        {
            Check.Require(!string.IsNullOrWhiteSpace(rationale), "rationale is required.");

            var project = GetProject(id, userId);

            if (elicitationType.SquareType.Name == SquareTypes.Security)
            {
                project.SecurityElicitationType = elicitationType;
                project.SecurityElicitationRationale = rationale;
            }

            if (elicitationType.SquareType.Name == SquareTypes.Privacy)
            {
                project.PrivacyElicitationType = elicitationType;
                project.PrivacyElicitationRationale = rationale;
            }

            db.SaveChanges();
        }
        #endregion

        #region Step 6 Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <param name="squareType"></param>
        /// <param name="requirement"></param>
        /// <param name="modelState"></param>
        public void SaveRequirement(int id /* project id */, SquareType squareType, Requirement requirement, ModelStateDictionary modelState)
        {
            Check.Require(requirement != null, "requirement is required.");
            Check.Require(squareType != null, "squareType is required.");

            var project = db.Projects.Where(a => a.Id == id).FirstOrDefault();

            requirement.Project = project;
            requirement.SquareType = squareType;

            //Validation.Validate(requirement, modelState);

            if (modelState.IsValid)
            {
                if (requirement.Id <= 0) db.Requirements.Add(requirement);

                db.SaveChanges();
            }
        }

        public void DeleteRequirement(int id, int requirementId)
        {
            var requirement = db.Requirements.Where(a => a.Id == requirementId).Single();

            Check.Ensure(requirement.Project.Id == id, "Requirement mismatch, does not match project id.");

            db.Requirements.Remove(requirement);

            db.SaveChanges();
        }
        #endregion

        #region Project Status
        /// <summary>
        /// Determines the status of the requested step
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <returns></returns>
        public ProjectStepStatus GetStepStatus(int id = -1, ProjectStep projectStep = null)
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

        /// <summary>
        /// Determines if step is in working state
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <returns></returns>
        public bool IsStepWorking(int id)
        {
            return GetStepStatus(id) == ProjectStepStatus.Working;
        }

        /// <summary>
        /// Determines if step is in pending state
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <returns></returns>
        public bool IsStepPending(int id)
        {
            return GetStepStatus(id) == ProjectStepStatus.Pending;
        }

        /// <summary>
        /// Determines if step is in complete state
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <returns></returns>
        public bool IsStepComplete(int id)
        {
            return GetStepStatus(id) == ProjectStepStatus.Complete;
        }

        /// <summary>
        /// Determines if the step can/should be edited according to definition of SQUARE
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <returns></returns>
        public bool CanStepChangeStatus(int id = -1, ProjectStep projectStep = null)
        {
            var step = projectStep ?? db.ProjectSteps.Where(a => a.Id == id).SingleOrDefault();

            Check.Require(step != null, "step is required.");

            var stp = step.Step.Order;
            var squareType = step.Step.SquareType;
            var projectSteps = step.Project.ProjectSteps.ToList();

            return ValidatePrereqs(stp, squareType, projectSteps)
                && ValidateDependancies(stp, squareType, projectSteps);
        }

        private bool ValidatePrereqs(int step, SquareType squareType, List<ProjectStep> projectSteps)
        {
            // 0 index array with status at each step, ex. step 1 --> [0] contains the status of the step
            var steps = projectSteps.Where(a => a.Step.SquareType == squareType).OrderBy(a => a.Step.Order)
                                    .Select(a => GetStepStatus(projectStep:a)).ToArray();

            Check.Require(steps.Count() == 9, "Incorrect number of steps returned.");

            switch (step)
            {
                case 1: return true;                                    // no prereqs
                case 2: return steps[0] == ProjectStepStatus.Complete;  // step 1
                case 3: return true;                                    // no prereqs
                case 4: return steps[2] == ProjectStepStatus.Complete;  // step 3
                case 5: return steps[0] == ProjectStepStatus.Complete   // step 1
                            && steps[1] == ProjectStepStatus.Complete;  // step 2
                case 6: return steps[2] == ProjectStepStatus.Complete   // step 3
                            && steps[3] == ProjectStepStatus.Complete   // step 4
                            && steps[4] == ProjectStepStatus.Complete;  // step 5
                case 7: return steps[5] == ProjectStepStatus.Complete;  // step 6
                case 8: return steps[3] == ProjectStepStatus.Complete   // step 4
                            && steps[6] == ProjectStepStatus.Complete;  // step 7
                case 9: return steps[7] == ProjectStepStatus.Complete;  // step 8
            }

            throw new ArgumentException("Step was not a valid step.");
        }

        private bool ValidateDependancies(int step, SquareType squareType, List<ProjectStep> projectSteps)
        {
            // 0 index array with status at each step, ex. step 1 --> [0] contains the status of the step
            var steps = projectSteps.Where(a => a.Step.SquareType == squareType).OrderBy(a => a.Step.Order)
                                    .Select(a => GetStepStatus(-1, a)).ToArray();

            Check.Require(steps.Count() == 9, "Incorrect number of steps returned.");

            switch (step)
            {
                case 1: return steps[1] != ProjectStepStatus.Complete   // step 2
                            && steps[4] != ProjectStepStatus.Complete;  // step 5
                case 2: return steps[4] != ProjectStepStatus.Complete;  // step 5
                case 3: return steps[3] != ProjectStepStatus.Complete   // step 4
                            && steps[5] != ProjectStepStatus.Complete;  // step 6
                case 4: return steps[5] != ProjectStepStatus.Complete   // step 6
                            && steps[7] != ProjectStepStatus.Complete;  // step 8
                case 5: return steps[5] != ProjectStepStatus.Complete;  // step 6
                case 6: return steps[6] != ProjectStepStatus.Complete;  // step 7
                case 7: return steps[7] != ProjectStepStatus.Complete;  // step 8
                case 8: return steps[8] != ProjectStepStatus.Complete;  // step 9
                case 9: return true;                                    // no prereqs
            }

            throw new ArgumentException("Step was not a valid step.");
        }

        public ProjectStep UpdateStatus(int id, ProjectStepStatus projectStepStatus, string login)
        {
            var step = db.ProjectSteps.Where(a => a.Id == id).Single();

            // validate that the step can be changed
            if (CanStepChangeStatus(projectStep: step))
            {
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
            }

            db.SaveChanges();

            return step;
        }

        #endregion
    }
}