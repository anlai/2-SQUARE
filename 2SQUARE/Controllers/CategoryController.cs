using System;
using System.Data.Objects;
using System.Security;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Helpers;
using _2SQUARE.Models;
using _2SQUARE.Services;
using MvcContrib;
using System.Linq;

namespace _2SQUARE.Controllers
{
    public class CategoryController : ApplicationController, IProcedureController
    {
        private readonly IProjectService _projectService;

        public CategoryController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public ActionResult Index(int id, int projectId)
        {
            try
            {
                var viewModel = CategoryViewModel.Create(Db, _projectService, projectId, id, CurrentUserId);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        public ActionResult Create(int id, int projectId)
        {
            try
            {
                var viewModel = CategoryEditViewModel.Create(Db, _projectService, projectId, id, CurrentUserId);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }
        
        [HttpPost]
        public ActionResult Create(int id, int projectId, Category category)
        {
            try
            {
                var projectStep = _projectService.GetProjectStep(id, CurrentUserId);

                category.SquareTypeId = projectStep.Step.SquareTypeId;
                category.ProjectId = projectId;
                Validation.Validate(category, ModelState);

                if (ModelState.IsValid)
                {
                    Db.Categories.AddObject(category);
                    Db.SaveChanges();

                    Message = string.Format(Messages.Saved, "Category");
                    return this.RedirectToAction(a => a.Index(id, projectId));
                }

                var viewModel = CategoryEditViewModel.Create(Db, _projectService, projectId, id, CurrentUserId, category);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        public ActionResult Edit(int id, int projectId, int categoryId)
        {
            try
            {
                var category = Db.Categories.Where(a => a.id == categoryId).SingleOrDefault();

                if (category == null)
                {
                    Message = string.Format(Messages.UnabletoLoad, "category", categoryId);
                    return this.RedirectToAction(a => a.Index(id, projectId));
                }

                var viewModel = CategoryEditViewModel.Create(Db, _projectService, projectId, id, CurrentUserId, category);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, int projectId, int categoryId, Category category)
        {
            try
            {
                var origCategory = Db.Categories.Where(a => a.id == categoryId).SingleOrDefault();

                if (origCategory == null)
                {
                    Message = string.Format(Messages.UnabletoLoad, "category", categoryId);
                    return this.RedirectToAction(a => a.Index(id, projectId));
                }

                origCategory.Name = category.Name;

                Validation.Validate(origCategory, ModelState);

                if (ModelState.IsValid)
                {
                    Db.SaveChanges();
                    Db.Refresh(RefreshMode.StoreWins, origCategory);

                    Message = string.Format(Messages.Saved, "Category");
                    return this.RedirectToAction(a => a.Index(id, projectId));
                }

                var viewModel = CategoryEditViewModel.Create(Db, _projectService, projectId, id, CurrentUserId, origCategory);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.Security(string.Format(Messages.NoAccess, "project")));
            }
        }

        [HttpPost]
        public ActionResult Delete(int id, int projectId, int categoryId)
        {
            var category = Db.Categories.Where(a => a.id == categoryId).SingleOrDefault();

            if (category == null)
            {
                Message = string.Format(Messages.UnabletoLoad, "category", categoryId);
                return this.RedirectToAction(a => a.Index(id, projectId));
            }

            Db.Categories.DeleteObject(category);
            Db.SaveChanges();

            Message = string.Format(Messages.Deleted, "category");
            return this.RedirectToAction(a => a.Index(id, projectId));

        }

    }

   
}
