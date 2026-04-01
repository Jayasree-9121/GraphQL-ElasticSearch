using GraphQL.Types;
using GraphQLDemo.Models;

namespace GraphQLDemo.GraphQL
{
    public class AIResultType : ObjectGraphType<AIResult>
    {
        public AIResultType()
        {
            Field<ListGraphType<PlanningResultType>>(
                "planning",
                resolve: context => context.Source.Planning ?? new List<PlanningResult>()
            );

            Field<ListGraphType<ErrorAnalysisResultType>>(
                "analysis",
                resolve: context => context.Source.Analysis ?? new List<ErrorAnalysisResult>()
            );
        }
    }
}
