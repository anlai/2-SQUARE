using System.Collections.Generic;
using _2SQUARE.Models;

namespace _2SQUARE.Services
{
    public interface IProjectService
    {
        bool HasAccess(int id, string login);
        List<string> UserRoles(int id, string login);
        bool IsInProjectRole(int id /* project id */, string login, string roleName);
        IList<Project> GetByUser(string login);
        Project GetProject(int id, string login);
        ProjectTerm AddTermToProject(int id, int squareTypeId, string term = null, string definition = null, string source = null, int termId = 0, int definitionId = 0);
    }
}