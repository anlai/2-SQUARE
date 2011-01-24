using System.Collections.Generic;
using System.Linq;
using _2SQUARE.Helpers;
using _2SQUARE.Services;

namespace _2SQUARE.Models
{
    public class ChangeStatusViewModel
    {
        public Project Project { get; set; }
        public List<KeyValuePair<int, string>> Status { get; set; }
        public List<ChangeStatusProjectStep> ChangeStatusProjectSteps { get; set; }
        public List<SquareType> SquareTypes { get; set; }

        public static ChangeStatusViewModel Create(Project project, IProjectService projectService)
        {
            var viewModel = new ChangeStatusViewModel() {
                Project = project, 
                Status = new List<KeyValuePair<int, string>>(),
                ChangeStatusProjectSteps = new List<ChangeStatusProjectStep>(),
                SquareTypes = project.ProjectSteps.Select(a => a.Step.SquareType).Distinct().ToList()
            };

            // add the 3 status'
            viewModel.Status.Add(new KeyValuePair<int, string>((int)ProjectStepStatus.Pending, ProjectStepStatus.Pending.ToString()));
            viewModel.Status.Add(new KeyValuePair<int, string>((int)ProjectStepStatus.Working, ProjectStepStatus.Working.ToString()));
            viewModel.Status.Add(new KeyValuePair<int, string>((int)ProjectStepStatus.Complete, ProjectStepStatus.Complete.ToString()));

            // populate the list of change status project steps
            viewModel.ChangeStatusProjectSteps = project.ProjectSteps.Select(a => new ChangeStatusProjectStep()
                                                            {
                                                                ProjectStepId = a.Id,
                                                                Order = a.Step.Order,
                                                                SquareTypeId = a.Step.SquareTypeId,
                                                                Name = a.Step.Name,
                                                                CurrentStepStatus = projectService.GetStepStatus(projectStep:a),
                                                                CanEdit = projectService.CanStepChangeStatus(a.Id)
                                                            }).ToList();

            return viewModel;
        }
    }

    public class ChangeStatusProjectStep
    {
        public int ProjectStepId { get; set; }
        public int Order { get; set; }
        public int SquareTypeId { get; set; }
        public string Name { get; set; }
        public ProjectStepStatus CurrentStepStatus { get; set; }
        public bool CanEdit { get; set; }
    }
}