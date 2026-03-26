using GraphQLDemo.Models;

namespace GraphQLDemo.Contracts
{
    public interface IPlanningService
    {
        Task<PlanningResult> RunPlanningAgent(ErrorLog errorLog);
    }
}
