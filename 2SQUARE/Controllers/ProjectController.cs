﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.Services;
using UCDArch.Web.Controller;

namespace _2SQUARE.Controllers
{
    public class ProjectController : SuperController
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        //
        // GET: /Project/
        [Authorize]
        public ActionResult Index()
        {
            var projects = _projectService.GetByUser(CurrentUser.Identity.Name);

            return View(projects);
        }

    }
}
