using System;
using System.Data.Objects;
using System.Security;
using System.Web.Mvc;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Core.Domain;
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

        /// <summary>
        /// Displays a list of categories for the current project
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <returns></returns>
        public ActionResult Index(int id, int projectId)
        {
            try
            {
                var viewModel = CategoryViewModel.Create(Db, _projectService, projectId, id, CurrentUserId);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.NoAccessToStep());
            }
        }

        /// <summary>
        /// Create a new category
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <returns></returns>
        public ActionResult Create(int id, int projectId)
        {
            try
            {
                var viewModel = CategoryEditViewModel.Create(Db, _projectService, projectId, id, CurrentUserId);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.NoAccessToStep());
            }
        }
        
        /// <summary>
        /// Create a new category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="projectId"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(int id, int projectId, Category category)
        {
            try
            {
                var project = _projectService.GetProject(projectId, CurrentUserId);

                if (ModelState.IsValid)
                {
                    _projectService.SaveCategory(id, category);

                    Message = string.Format(Messages.Saved, "Category");
                    return this.RedirectToAction(a => a.Index(id, projectId));    
                }

                var viewModel = CategoryEditViewModel.Create(Db, _projectService, projectId, id, CurrentUserId, category);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.NoAccessToStep());
            }
        }

        /// <summary>
        /// Edit a requirement category
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <param name="categoryId">Category Id to edit</param>
        /// <returns></returns>
        public ActionResult Edit(int id, int projectId, int categoryId)
        {
            try
            {
                var category = Db.Categories.Where(a => a.Id == categoryId).Single();

                var viewModel = CategoryEditViewModel.Create(Db, _projectService, projectId, id, CurrentUserId, category);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.NoAccessToStep());
            }
        }

        /// <summary>
        /// Edit a requirement category
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <param name="categoryId">Category Id to edit</param>
        /// <param name="category">Category (containing new value)</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int id, int projectId, int categoryId, Category category)
        {
            try
            {
                var project = _projectService.GetProject(projectId, CurrentUserId);

                if (ModelState.IsValid)
                {
                    _projectService.SaveCategory(id, category, categoryId);

                    Message = string.Format(Messages.Saved, "Category");
                    return this.RedirectToAction(a => a.Index(id, projectId));
                }

                var viewModel = CategoryEditViewModel.Create(Db, _projectService, projectId, id, CurrentUserId, category);
                return View(viewModel);
            }
            catch (SecurityException)
            {
                return this.RedirectToAction<ErrorController>(a => a.NoAccessToStep());
            }
        }

        /// <summary>
        /// Delete the category
        /// </summary>
        /// <param name="id">Project Step Id</param>
        /// <param name="projectId">Project Id</param>
        /// <param name="categoryId">Category Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id, int projectId, int categoryId)
        {
            _projectService.DeleteCategory(projectId, categoryId, CurrentUserId);

            Message = string.Format(Messages.Deleted, "category");
            return this.RedirectToAction(a => a.Index(id, projectId));
        }

    }

   
}
