using System.Collections.Generic;
using System.Web.Mvc;
using _2SQUARE.Core.Domain;
using _2SQUARE.Helpers;

namespace _2SQUARE.Services
{
    public interface IProjectService
    {
        #region Access Methods
        bool HasAccess(int id, string login);
        List<string> UserRoles(int id, string login);
        bool IsInProjectRole(int id /* project id */, string login, string roleId);
        IList<Project> GetByUser(string login);
        Project GetProject(int id, string login);
        ProjectStep GetProjectStep(int id, string login);
        IList<ProjectStep> GetProjectSteps(int id, SquareType squareType = null);
        #endregion

        Project CreateProject(string name, string description, string login);

        #region Step 1 Methods
        ProjectTerm AddTermToProject(int id, int squareTypeId, string term = null, string definition = null, string source = null, int termId = 0, int definitionId = 0);
        ProjectTerm UpdateProjectTerm(int id, int projectId, ModelStateDictionary modelState, string term = null, string definition = null, string source = null, int? definitionId = null);
        #endregion

        #region Step 2 Methods
        Goal LoadGoal(int id);
        Goal SaveGoal(int id /* projectStep Id */, Goal goal, int? goalId = null, string goalTypeId = null);
        void DeleteGoal(int id /* goal id */);
        #endregion
        
        #region Step 3 Methods
        Artifact LoadArtifact(int id);
        Artifact SaveArtifact(int id, Artifact artifact, int? artifactId = null, int? artifactTypeId = null);
        void DeleteArtifact(int id /* artifact id*/);
        #endregion

        #region Step 4 Methods
        void SetAssessmentType(int id /* project id */, int assessmentTypeId, string userId);
        #endregion

        #region Step 5 Methods
        void SetElicitationType(int id, int elicitationTypeId, string rationale, string userId);
        #endregion

        #region Step 6 Methods
        void SaveRequirement(int id /* project id */, SquareType squareType, Requirement requirement, ModelStateDictionary modelState);
        void DeleteRequirement(int id, int requirementId);
        #endregion

        #region Step Status Methods
        ProjectStepStatus GetStepStatus(int id = -1, ProjectStep projectStep = null);
        bool IsStepWorking(int id);
        bool IsStepPending(int id);
        bool IsStepComplete(int id);
        bool CanStepChangeStatus(int id = -1, ProjectStep projectStep = null);
        ProjectStep UpdateStatus(int id, ProjectStepStatus projectStepStatus, string login);
        #endregion
    }
}