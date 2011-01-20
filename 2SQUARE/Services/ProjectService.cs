﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Models;
using DesignByContract;

namespace _2SQUARE.Services
{
    public class ProjectService : IProjectService
    {
        SquareEntities db = new SquareEntities();

        #region Access Methods
        public bool HasAccess(int id, string login)
        {
            Check.Require(!string.IsNullOrEmpty(login), "login is required.");

            return db.ProjectWorkers.Where(a => a.ProjectId == id && a.aspnet_Users.UserName == login).Any();
        }

        public List<string> UserRoles(int id, string login)
        {
            Check.Require(!string.IsNullOrEmpty(login), "login is required.");

            return
                db.ProjectWorkers.Where(a => a.ProjectId == id && a.aspnet_Users.UserName == login).Select(
                    a => a.aspnet_Roles.RoleName).ToList();
        }

        public bool IsInProjectRole(int id /* project id */,string login, string roleName)
        {
            return
                db.ProjectWorkers.Where(a => a.ProjectId == id && a.aspnet_Users.UserName == login && a.aspnet_Roles.RoleName == roleName)
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

            var user = db.aspnet_Users.Where(a => a.UserName == login).Single();

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

            var user = db.aspnet_Users.Where(a => a.UserName == login).Single();
            var project = db.Projects.Where(a => a.id == id).Single();

            if (!project.ProjectWorkers.Any(a => a.aspnet_Users.UserId == user.UserId)) throw new SecurityException(string.Format(Messages.NoAccess, "Project(id="+id+")"));

            return project;
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
        public ProjectTerm AddTermToProject(int id, int squareTypeId, string term = null, string definition = null, string source = null, int termId = 0, int definitionId = 0)
        {
            // update existing project term
            if (termId > 0 && definitionId > 0)
            {
                var termObj = db.Terms.Where(a => a.id == termId).Single();
                var definitionObj = db.Definitions.Where(a => a.id == definitionId).Single();

                // make sure def matches term
                if (definitionObj.TermId != termObj.id) throw new ArgumentException("Term/Definition mismatch.");

                term = termObj.Name;
                definition = definitionObj.Description;
                source = definitionObj.Source;
            }

            if (!string.IsNullOrEmpty(term) && !string.IsNullOrEmpty(definition) && !string.IsNullOrEmpty(source))
            {
                if (id <= 0 || squareTypeId <= 0) throw new ArgumentException("Project or Square Type Id are invalid");

                var projectTerm = new ProjectTerm();
                projectTerm.Term = term;
                projectTerm.Definition = definition;
                projectTerm.Source = source;
                projectTerm.ProjectId = id;
                projectTerm.SquareTypeId = squareTypeId;

                db.AddToProjectTerms(projectTerm);

                db.SaveChanges();

                return projectTerm;
            }

            return null;
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

        #endregion
    }
}