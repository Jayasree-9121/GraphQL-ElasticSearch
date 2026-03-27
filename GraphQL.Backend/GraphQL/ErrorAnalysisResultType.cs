using GraphQL.Types;
using GraphQLDemo.Models;

namespace GraphQLDemo.GraphQL
{
    public class ErrorAnalysisResultType : ObjectGraphType<ErrorAnalysisResult>
    {
        public ErrorAnalysisResultType()
        {
            Field(x => x.ErrorType, nullable: true);
            Field(x => x.ErrorSummary, nullable: true);

            Field<ListGraphType<StringGraphType>>("possibleCauses");
            Field<ListGraphType<StringGraphType>>("occurrenceConditions");

            // ✅ ADD THESE TWO (YOUR ERROR FIX)
            Field(x => x.BusinessImpact, nullable: true);
            Field(x => x.Severity, nullable: true);

            Field<ListGraphType<StringGraphType>>("fixSuggestions");
            Field(x => x.RecommendedFix, nullable: true);
        }
    }
}
