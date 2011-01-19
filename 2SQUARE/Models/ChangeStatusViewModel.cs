using System.Collections.Generic;

namespace _2SQUARE.Models
{
    public class ChangeStatusViewModel
    {
        public Project Project { get; set; }
        public List<KeyValuePair<string, string>> Status { get; set; }

        public static ChangeStatusViewModel Create(Project project)
        {
            var viewModel = new ChangeStatusViewModel() {Project = project};

            //// add the 3 status'
            //viewModel.Status.Add(new KeyValuePair<string, string>("P", "Pending"));
            //viewModel.Status.Add(new KeyValuePair<string, string>("W", "Working"));
            //viewModel.Status.Add(new KeyValuePair<string, string>("C", "Complete"));

            //return viewModel;
        }
    }
}