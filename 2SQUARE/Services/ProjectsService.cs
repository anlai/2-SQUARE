using System;
using System.Collections.Generic;
using System.Web.Mvc;
using _2SQUARE.Core.Domain;
using _2SQUARE.Helpers;
using System.Linq;
using DesignByContract;

namespace _2SQUARE.Services
{
    public class ProjectsService : IProjectsService
    {
        /// <summary>
        /// Does the user have access?
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <param name="login">User's Login Id</param>
        /// <returns></returns>
        public bool HasAccess(int id, string login)
        {
            Check.Require(!string.IsNullOrWhiteSpace(login), "login is required.");

            using (var db = new SquareContext())
            {
                var project = db.Projects.Where(a => a.Id == id).Single();
                var user = db.Users.Where(a => a.Username == login).Single();

                return project.ProjectWorkers.Where(a => a.User == user).Any();
            }
        }

        public List<string> UserRoles(int id, string login)
        {
            throw new NotImplementedException();
        }

        public bool IsInProjectRole(int id, string login, string roleName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets projects available to user
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public IList<Project> GetByUser(string login)
        {
            Check.Require(!string.IsNullOrWhiteSpace(login), "login is required.");

            using (var db = new SquareContext())
            {
                var user = db.Users.Where(a => a.Username == login).Single();

                var projects = db.Projects.Where(a => a.ProjectWorkers.Select(b => b.User.UserId).Contains(user.UserId));

                return projects.ToList();
            }
        }

        public Project GetProject(int id, string login)
        {
            throw new NotImplementedException();
        }

        public ProjectStep GetProjectStep(int id, string login)
        {
            throw new NotImplementedException();
        }

        public IList<ProjectStep> GetProjectSteps(int id, SquareType squareType)
        {
            throw new NotImplementedException();
        }

        public ProjectTerm AddTermToProject(int id, int SquareType, string term, string definition, string source, int termId, int definitionId)
        {
            throw new NotImplementedException();
        }

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

        public ProjectStepStatus GetStepStatus(int id, ProjectStep projectStep)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}