using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _2SQUARE.App_Data;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;

namespace _2SQUARE.Services
{
    public class ProjectService : IProjectService
    {
        //private readonly IRepository<Project> _projectRepository;
        //private readonly IRepository<User> _userRepository;

        //public ProjectService(IRepository<Project> projectRepository, IRepository<User> userRepository)
        //{
        //    _projectRepository = projectRepository;
        //    _userRepository = userRepository;
        //}

        _2SquareDataDataContext db = new _2SquareDataDataContext();

        public IList<Project> GetByUser(string login)
        {
            Check.Require(login != null, "login is required.");

            var user = db.aspnet_Users.Where(a => a.UserName == login).Single();

            return user.Projects;
            
        }
    }
}