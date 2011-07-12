using _2SQUARE.Core.Domain;
using _2SQUARE.Models;
using DesignByContract;

namespace _2SQUARE.Models
{
    public class RiskRecommendationViewModel
    {
        public RiskRecommendation RiskRecommendation { get; set; }
        public Risk Risk { get; set; }
        public int ProjectStepId { get; set; }

        public static RiskRecommendationViewModel Create(int projectStepId, Risk risk, RiskRecommendation riskRecommendation = null)
        {
            Check.Require(risk != null, "risk is required.");

            var viewModel = new RiskRecommendationViewModel()
                                {
                                    ProjectStepId = projectStepId,
                                    RiskRecommendation = riskRecommendation ?? new RiskRecommendation(),
                                    Risk = risk
                                };

            return viewModel;
        }
    }
}