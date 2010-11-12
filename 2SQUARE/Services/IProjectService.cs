using System.Collections.Generic;
using _2SQUARE.Models;

namespace _2SQUARE.Services
{
    public interface IProjectService
    {
        IList<Project> GetByUser(string login);
    }
}