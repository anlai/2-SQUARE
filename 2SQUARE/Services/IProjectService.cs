﻿using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.Core.Domain;
using _2SQUARE.Helpers;

namespace _2SQUARE.Services
{
    public interface IProjectService
    {
        #region Access Methods
        bool HasAccess(int id, string login, int? projectStepId = null);
        bool HasStepAccess(int projectStepId, string login);
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

        void CreateRisk(int id, int squareTypeId, string userId, string name, string source, string vulnerability, string riskLevelId);
        void RemoveRisk(int id);
        #endregion

        #region Step 5 Methods
        void SetElicitationType(int id, int elicitationTypeId, string rationale, string userId);
        #endregion

        #region Step 6 Methods
        void SaveRequirement(int id /* project id */, int squareTypeId, Requirement requirement, int? requirementId = null);
        void DeleteRequirement(int id, int requirementId, string loginId);
        #endregion

        #region Step 7 Methods
        /// <summary>
        /// Save a requirement category
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="category">Category</param>
        /// <param name="categoryId">Category Id, used if updating</param>
        void SaveCategory(int id, Category category, int? categoryId = null);
        /// <summary>
        /// Delete a requirement category
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <param name="categoryId">Category Id to Delete</param>
        /// <param name="loginId">User's Login Id</param>
        void DeleteCategory(int id, int categoryId, string loginId);
        /// <summary>
        /// Set the category for a requirement
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <param name="categoryId">Category id</param>
        /// <param name="requirementId">Requirement Id</param>
        /// <param name="essential">Is this requirement essential?</param>
        /// <param name="loginId">User's Login Id</param>
        void CategorizeRequirement(int id, int categoryId, int requirementId, bool essential, string loginId);
        #endregion

        #region Step 8 Methods
        /// <summary>
        /// Set the order of requirements for a project's square type
        /// </summary>
        /// <param name="id">Project Id</param>
        /// <param name="squareTypeId">Square Type Id</param>
        /// <param name="requirementIds">List of reuqirements ids in order</param>
        void UpdateRequirementOrder(int id, int squareTypeId, int[] requirementIds, string loginId);
        /// <summary>
        /// Update a requirement's id
        /// </summary>
        /// <param name="id">Requirement Id</param>
        /// <param name="priorityTypeId">Priority Type Id</param>
        void UpdateRequirementPriority(int id, int? priorityTypeId, string loginId);
        #endregion

        #region Step 9 Methods
        /// <summary>
        /// Save a requirement defect
        /// </summary>
        /// <param name="projectId">Project Id</param>
        /// <param name="requirementId">Requirement Id</param>
        /// <param name="defect">Identified Defect</param>
        /// <param name="loginId">User's Login</param>
        void SaveDefect(int projectId, int requirementId, string defect, string loginId);
        /// <summary>
        /// Resolves an associated defect
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="defectId"></param>
        /// <param name="loginId"></param>
        void ResolveDefect(int projectId, int defectId, string loginId);
        #endregion

        #region Notes
        ProjectStepNote AddNoteToProjectStep(int id, string note, string userId);
        #endregion

        #region Project Step File
        ProjectStepFile AddFileToProjectStep(int id, string note, string filename, string contenttype, byte[] contents, string userId);
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