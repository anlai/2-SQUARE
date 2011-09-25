using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.Core.Domain;
using _2SQUARE.Services;

namespace _2SQUARE.Controllers
{
    public class ProjectStepNoteController : ApplicationController
    {
        private readonly IProjectService _projectService;

        public ProjectStepNoteController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// Add a note to a project step
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="notes">Note Text</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveNotes(int id, string notes)
        {
            try
            {
                var note = _projectService.AddNoteToProjectStep(id, notes, CurrentUserId);

                return Json(new {id=note.Id, note=note.Description, dateCreated=note.DateCreated.ToString("d"), user=note.UserId});
            }
            catch (Exception ex)
            {
                return Json(false);
            }
            
        }

        /// <summary>
        /// Remove a note from a project step
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="noteId">Note Id to Delete</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteNotes(int id, int noteId)
        {
            var note = Db.ProjectStepNotes.Where(a => a.Id == noteId).FirstOrDefault();

            if (note != null)
            {
                Db.ProjectStepNotes.Remove(note);
                Db.SaveChanges();

                return Json(true);
            }

            return Json(false);
        }

    }
}
