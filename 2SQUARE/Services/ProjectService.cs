using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using _2SQUARE.Models;

namespace _2SQUARE.Services
{
    public class ProjectService : IProjectService
    {
        SquareEntities db = new SquareEntities();

        /// <summary>
        /// Returns a list of projects based on user id
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public IList<Project> GetByUser(string login)
        {
            var user = db.aspnet_Users.Where(a => a.UserName == login).Single();

            return user.ProjectWorkers.Select(a=>a.Project).ToList();
        }

        /// <summary>
        /// Returns a project if a user has access to the project
        /// </summary>
        /// <param name="id"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        public Project GetProject(int id, string login)
        {
            var user = db.aspnet_Users.Where(a => a.UserName == login).Single();
            var project = db.Projects.Where(a => a.id == id).Single();

            if (!project.ProjectWorkers.Any(a => a.aspnet_Users == user)) throw new SecurityException(string.Format(Messages.NoAccess, "Project(id="+id+")"));

            return project;
        }
    }
}