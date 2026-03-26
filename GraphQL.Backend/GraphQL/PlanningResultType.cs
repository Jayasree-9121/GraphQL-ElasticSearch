using GraphQL.Types;
using GraphQLDemo.Models;

namespace GraphQLDemo.GraphQL
{
    public class PlanningResultType : ObjectGraphType<PlanningResult>
    {
        public PlanningResultType()
        {
            Field(x => x.ErrorCategory);
            Field<ListGraphType<StringGraphType>>("technologies");
            Field<ListGraphType<StringGraphType>>("investigationSteps");
            Field<ListGraphType<StringGraphType>>("searchQueries");
        }
    }
}
