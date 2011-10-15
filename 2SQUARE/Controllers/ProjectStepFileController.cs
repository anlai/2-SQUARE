using System;
using System.IO;
using System.Linq;
using System.Security;
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

        [HttpPost]
        public JsonResult SaveFile(int id)//, string notes)//, HttpPostedFileBase filedata)
        {
            // test
            

            try
            {
                var request = ControllerContext.HttpContext.Request;
                var notes = request["notes"];
                var qqfile = request["qqfile"];
                var contentType = request.Headers["X-File-Type"];

                byte[] contents;

                using (var reader = new BinaryReader(request.InputStream))
                {
                    contents = reader.ReadBytes((int)request.InputStream.Length);
                }

                var psfile = _projectService.AddFileToProjectStep(id, notes, qqfile, contentType, contents, CurrentUserId);

                return Json(new {id=psfile.Id, notes=psfile.Notes, dateCreated=psfile.DateCreated.ToString("d"), fileName=psfile.FileName, success = true});
            }
            catch (Exception ex)
            {
                return Json(false);
            }

            return Json(false);
        }

        /// <summary>
        /// Delete a project step file
        /// </summary>
        /// <param name="id">Project Step ID</param>
        /// <param name="fileId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteFile(int id, int fileId)
        {
            try
            {
                var ps = _projectService.GetProjectStep(id, CurrentUserId);

                var file = Db.ProjectStepFiles.Where(a => a.Id == fileId).FirstOrDefault();
                if (file != null)
                {
                    Db.ProjectStepFiles.Remove(file);
                    Db.SaveChanges();
                    return Json(true);
                }
            }
            catch (Exception)
            {
                return Json(false);
            }

            return Json(false);
        }

        public FileResult GetFile()
        {
            throw new NotImplementedException();
        }

    }

}
