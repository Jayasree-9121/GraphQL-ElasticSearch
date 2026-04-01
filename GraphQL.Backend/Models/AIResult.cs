namespace GraphQLDemo.Models
{
    public class AIResult
    {
        public List<PlanningResult> Planning { get; set; }
        public List<ErrorAnalysisResult> Analysis { get; set; }
    }
}
