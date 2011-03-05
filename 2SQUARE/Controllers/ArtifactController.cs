using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
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
            var viewModel = ArtifactViewModel.Create(Db, null, id);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(int id /* project step id*/, Artifact artifact, HttpPostedFileBase file)
        {
            // only validate hte user's input
            Validation.Validate(artifact, ModelState);
            
            if (file != null)
            {
                // read the file
                var reader = new BinaryReader(file.InputStream);
                var data = reader.ReadBytes(file.ContentLength);
                artifact.Data = data;
            }

            if (ModelState.IsValid)
            {
                Message = string.Format(Messages.Saved, "Artifact");
                _projectService.SaveArtifact(id, artifact, CurrentUserId);
                return LinkGenerator.CreateRedirectResult(Request.RequestContext, _projectService.GetProjectStep(id, CurrentUserId));
            }

            var viewModel = ArtifactViewModel.Create(Db, artifact, id);
            return View(viewModel);
        }

        public ActionResult Edit(int id /* project step id */, int artifactId)
        {
            var artifact = _projectService.LoadArtifact(artifactId);
            var projectStep = _projectService.GetProjectStep(id, CurrentUserId);

            if (artifact == null) return LinkGenerator.CreateRedirectResult(Request.RequestContext, projectStep);

            var viewModel = ArtifactViewModel.Create(Db, artifact, id);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(int id /* project step id */, int artifactId, Artifact artifact, HttpPostedFileBase file)
        {
            // load existing
            var existingArtifact = _projectService.LoadArtifact(artifactId);

            // copy the new fields
            existingArtifact.Name = artifact.Name;
            existingArtifact.Description = artifact.Description;
            existingArtifact.ArtifactTypeId = artifact.ArtifactTypeId;

            // validate user's input
            Validation.Validate(existingArtifact, ModelState);

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
                _projectService.SaveArtifact(id, existingArtifact, CurrentUserId);
                return LinkGenerator.CreateRedirectResult(Request.RequestContext, _projectService.GetProjectStep(id, CurrentUserId));
            }

            var viewModel = ArtifactViewModel.Create(Db, existingArtifact, id);
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
    }
}
