﻿using System.Collections.Generic;
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
        #endregion

        #region Step 1 Methods
        ProjectTerm AddTermToProject(int id, int squareTypeId, string term = null, string definition = null, string source = null, int termId = 0, int definitionId = 0);
        #endregion

        #region Step Status Methods
        ProjectStepStatus GetStepStatus(int id = -1, ProjectStep projectStep = null);
        bool IsStepWorking(int id);
        bool IsStepPending(int id);
        bool IsStepComplete(int id);
        bool CanStepChangeStatus(int id = -1, ProjectStep projectStep = null);
        #endregion
    }

    public enum ProjectStepStatus {Pending=0, Working, Complete}
}