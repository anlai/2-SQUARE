using System.Collections.Generic;
using _2SQUARE.Models;

namespace _2SQUARE.Services
{
    public interface IProjectService
    {
        IList<Project> GetByUser(string login);
        Project GetProject(int id, string login);
        void AddTermToProject(int id, string term, string definition, string source, int projectTermId = 0);
    }
}