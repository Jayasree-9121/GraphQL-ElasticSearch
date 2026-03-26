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
            Field<ListGraphType<StringGraphType>>("fixSuggestions");
            Field(x => x.RecommendedFix, nullable: true);
        }
    }
}
