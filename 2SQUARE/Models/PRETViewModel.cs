using System.Collections.Generic;
using System.Linq;
using _2SQUARE.App_GlobalResources;
using _2SQUARE.Services;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class PRETViewModel : ViewModelBase
    {
        public IQueryable<PRETQuestion> PretQuestions { get; set; }
        public IList<PRETQuestionAnswer> QuestionAnswers { get; set; }

        public static PRETViewModel Create(SquareEntities db, IProjectService projectService, int projectId, int projectStepId, string currentUserId, bool loadQuestions= false, IList<PRETQuestionAnswer> questionAnswers = null)
        {
            Check.Require(projectService != null, "projectService is required.");

            var viewModel = new PRETViewModel() {QuestionAnswers = questionAnswers ?? new List<PRETQuestionAnswer>()} ;
            viewModel.SetProjectInfo(projectService, projectId, projectStepId, currentUserId);

            if (loadQuestions)
            {
                viewModel.PretQuestions = db.PRETQuestions;
            }

            Check.Ensure(viewModel.ProjectStep.Step.SquareType.Name == SquareTypes.Privacy, "PRET only works with privacy.");

            return viewModel;
        }
    }

    public class PRETQuestionAnswer
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public bool IsSubQuestion { get; set; }
    }
}