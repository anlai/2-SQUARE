using System;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.Services;

namespace _2SQUARE.Controllers
{
    public class ProjectStepFileController : ApplicationController
    {
        private readonly IProjectService _projectService;

        public ProjectStepFileController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public JsonResult SaveFile(int id, string notes, HttpPostedFileBase filedata)
        {
            var req = Request.Files;

            throw new NotImplementedException();
        }

        public JsonResult DeleteFile()
        {
            throw new NotImplementedException();
        }

        public FileResult GetFile()
        {
            throw new NotImplementedException();
        }

    }

}
