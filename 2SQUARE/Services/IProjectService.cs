﻿using System.Collections.Generic;
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
        IList<ProjectStep> GetProjectSteps(int id, SquareType squareType = null);
        #endregion

        #region Step 1 Methods
        ProjectTerm AddTermToProject(int id, int squareTypeId, string term = null, string definition = null, string source = null, int termId = 0, int definitionId = 0);
        #endregion

        #region Step 2 Methods
        Goal AddGoal(int id /* projectStep Id */, Goal goal);
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