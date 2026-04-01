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

            Field(x => x.PossibleCauses, type: typeof(ListGraphType<StringGraphType>));
            Field(x => x.OccurrenceConditions, type: typeof(ListGraphType<StringGraphType>));

            Field(x => x.BusinessImpact, nullable: true);
            Field(x => x.Severity, nullable: true);

            Field(x => x.FixSuggestions, type: typeof(ListGraphType<StringGraphType>));
            Field(x => x.RecommendedFix, nullable: true);
        }
    }
}