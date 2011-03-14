using System.Collections.Generic;
using _2SQUARE.Helpers;
using _2SQUARE.Models;

namespace _2SQUARE.Services
{
    public interface IProjectService
    {
        #region Access Methods
        bool HasAccess(int id, string login);
        List<string> UserRoles(int id, string login);
        bool IsInProjectRole(int id /* project id */, string login, string roleName);
        IList<Project> GetByUser(string login);
        Project GetProject(int id, string login);
        ProjectStep GetProjectStep(int id, string login);
        IList<ProjectStep> GetProjectSteps(int id, SquareType squareType = null);
        #endregion

        #region Step 1 Methods
        ProjectTerm AddTermToProject(int id, int squareTypeId, string term = null, string definition = null, string source = null, int termId = 0, int definitionId = 0);
        #endregion

        #region Step 2 Methods
        Goal LoadGoal(int id);
        Goal SaveGoal(int id /* projectStep Id */, Goal goal);
        void DeleteGoal(int id /* goal id */);
        #endregion
        
        #region Step 3 Methods
        Artifact LoadArtifact(int id);
        Artifact SaveArtifact(int id /* project step id */, Artifact artifact, string loginId);
        void DeleteArtifact(int id /* artifact id*/);
        #endregion

        #region Step 4 Methods
        void SetAssessmentType(int id /* project id */, AssessmentType assessmentType, string userId);
        RiskRecommendation SaveRiskRecommendation(RiskRecommendation riskRecommendation, int riskId);
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