using GraphQLDemo.Models;
using GraphQLDemo.Serivice;

namespace GraphQLDemo.Pipeline
{
    public class ErrorPipeline
    {
        private readonly PlanningAgentService _planner;
        private readonly ErrorAnalysisService _analysis;

        public ErrorPipeline(
            PlanningAgentService planner,
            ErrorAnalysisService analysis)
        {
            _planner = planner;
            _analysis = analysis;
        }

        public async Task<AIResult> Run(ErrorLog log)
        {
            var plan = await _planner.RunPlanningAgent(log);
            var analysis = await _analysis.Analyze(log);

            return new AIResult
            {
                Planning = plan,
                Analysis = analysis
            };
        }
    }
}
