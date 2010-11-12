﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _2SQUARE.App_Data;

namespace _2SQUARE.Services
{
    public class ProjectService : IProjectService
    {
        _2SquareDataDataContext db = new _2SquareDataDataContext();

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
    }
}