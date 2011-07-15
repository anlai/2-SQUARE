using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Core.Domain;
using _2SQUARE.Helpers;
using _2SQUARE.Models;
using _2SQUARE.Services;
using MvcContrib;

namespace _2SQUARE.Controllers
{
    public class ArtifactController : ApplicationController
    {
        private readonly IProjectService _projectService;

        public ArtifactController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// Create an artifact
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <returns></returns>
        public ActionResult Add(int id)
        {
            var viewModel = ArtifactViewModel.Create(Db, _projectService, null, id, CurrentUserId);
            return View(viewModel);
        }

        /// <summary>
        /// Create an artifact
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="artifact">Artifact Object</param>
        /// <param name="artifactTypeId">Artifact Type Id</param>
        /// <param name="file">File</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(int id, Artifact artifact, int artifactTypeId, HttpPostedFileBase file)
        {
            // validate access and load project step
            var projectStep = _projectService.GetProjectStep(id, CurrentUserId);

            ModelState.Remove("artifact.ArtifactType");
            ModelState.Remove("artifact.Project");
            ModelState.Remove("artifact.CreatedBy");

            if (file == null) ModelState.AddModelError("File", "File is required.");
            if (!Db.ArtifactTypes.Where(a => a.Id == artifactTypeId).Any())
                ModelState.AddModelError("Artifact Type", "Artifact Type is Required");


            if (ModelState.IsValid)
            {
                artifact.CreatedBy = CurrentUserId;

                // read the file
                var reader = new BinaryReader(file.InputStream);
                var data = reader.ReadBytes(file.ContentLength);
                artifact.Data = data;
                artifact.ContentType = file.ContentType;

                _projectService.SaveArtifact(id, artifact, null, artifactTypeId);
                Message = string.Format(Messages.Saved, "Artifact");
                return LinkGenerator.CreateRedirectResult(Request.RequestContext, _projectService.GetProjectStep(id, CurrentUserId));
            }

            var viewModel = ArtifactViewModel.Create(Db, _projectService, artifact, id, CurrentUserId);
            return View(viewModel);
        }

        public ActionResult Edit(int id /* project step id */, int artifactId)
        {
            var artifact = _projectService.LoadArtifact(artifactId);
            var projectStep = _projectService.GetProjectStep(id, CurrentUserId);

            if (artifact == null) return LinkGenerator.CreateRedirectResult(Request.RequestContext, projectStep);

            var viewModel = ArtifactViewModel.Create(Db, _projectService, artifact, id, CurrentUserId);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(int id /* project step id */, int artifactId, Artifact artifact, int artifactTypeId, HttpPostedFileBase file)
        {
            // load existing
            var existingArtifact = _projectService.LoadArtifact(artifactId);

            // copy the new fields
            existingArtifact.Name = artifact.Name;
            existingArtifact.Description = artifact.Description;
            existingArtifact.ArtifactType = artifact.ArtifactType;

            // validate user's input
            //Validation.Validate(existingArtifact, ModelState);

            // read the file and update if one was provided
            if (file != null)
            {
                // read the file
                var reader = new BinaryReader(file.InputStream);
                var data = reader.ReadBytes(file.ContentLength);
                existingArtifact.Data = data;
            }

            if (ModelState.IsValid)
            {
                Message = string.Format(Messages.Saved, "Artifact");
                _projectService.SaveArtifact(id, existingArtifact, artifactId, artifactTypeId);
                return LinkGenerator.CreateRedirectResult(Request.RequestContext, _projectService.GetProjectStep(id, CurrentUserId));
            }

            var viewModel = ArtifactViewModel.Create(Db, _projectService, existingArtifact, id, CurrentUserId);
            return View(viewModel);
        }

        public RedirectResult Delete(int id /* project step id */, int artifactId)
        {
            var projectStep = _projectService.GetProjectStep(id, CurrentUserId);

            // delete the artifact
            _projectService.DeleteArtifact(artifactId);

            // add a message
            Message = string.Format(Messages.Deleted, "Artifact");

            // redirect using the generator
            return LinkGenerator.CreateRedirectResult(Request.RequestContext, projectStep);
        }

        public ActionResult Details(int id /* project step id */, int artifactId)
        {
            var artifact = _projectService.LoadArtifact(artifactId);
            var projectStep = _projectService.GetProjectStep(id, CurrentUserId);

            if (artifact == null) return LinkGenerator.CreateRedirectResult(Request.RequestContext, projectStep);

            var viewModel = ArtifactViewModel.Create(Db, _projectService, artifact, id, CurrentUserId);
            return View(viewModel);
        }

        /// <summary>
        /// Returns the image for an artifact
        /// </summary>
        /// <param name="id">Artifact Id</param>
        /// <returns></returns>
        public FileContentResult GetFile(int id)
        {
            var artifact = _projectService.LoadArtifact(id);

            if (artifact == null) return File(new byte[0], string.Empty);

            return File(artifact.Data, artifact.ContentType);
        }
    }
}
