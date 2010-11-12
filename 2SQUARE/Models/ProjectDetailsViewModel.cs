﻿using System.Collections.Generic;
using System.Linq;
using _2SQUARE.Services;

namespace _2SQUARE.Models
{
    public class ProjectDetailsViewModel
    {
        public Project Project { get; set; }
        public IEnumerable<SquareType> SquareTypes { get; set; }
        public IEnumerable<ProjectStep> ProjectSteps { get; set; }

        public static ProjectDetailsViewModel Create(SquareEntities squareEntities, IProjectService projectService, int id, string loginId)
        {
            //var project = projectService.GetProject(id, loginId);

            //var viewModel = new ProjectDetailsViewModel();

            //viewModel.Project = project;
            //viewModel.SquareTypes = squareEntities.SquareTypes.ToList();
            //viewModel.ProjectSteps = squareEntities.ProjectSteps.Where(a => a.ProjectId == project.id).ToList();

            var viewModel = new ProjectDetailsViewModel()
                                {
                                    Project = projectService.GetProject(id, loginId),
                                    SquareTypes = squareEntities.SquareTypes.ToList()
                                    //ProjectSteps = squareEntities.ProjectSteps.Where(a=>a.Project.id == id).ToList()
                                };

            return viewModel;
        }
    }
}