namespace GraphQLDemo.Models
{
    public class ErrorAnalysisResult
    {
        public string ErrorType { get; set; }
        public string ErrorSummary { get; set; }
        public List<string> PossibleCauses { get; set; }
        public List<string> OccurrenceConditions { get; set; }
        public List<string> FixSuggestions { get; set; }
        public string RecommendedFix { get; set; }
    }
}
