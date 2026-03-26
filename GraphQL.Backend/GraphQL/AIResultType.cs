using GraphQL.Types;
using GraphQLDemo.Models;

namespace GraphQLDemo.GraphQL
{
    public class AIResultType : ObjectGraphType<AIResult>
    {
        public AIResultType()
        {
            Field<PlanningResultType>("planning");
            Field<ErrorAnalysisResultType>("analysis");
        }
    }
}
