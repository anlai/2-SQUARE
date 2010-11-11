using System.Collections.Generic;
using _2SQUARE.Core.Domain;

namespace _2SQUARE.Services
{
    public interface IProjectService
    {
        IList<Project> GetByUser(string login);
    }
}