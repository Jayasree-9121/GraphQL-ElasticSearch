using GraphQL.Types;
using GraphQLDemo.Models;

namespace GraphQLDemo.GraphQL
{
    public class AppInsightsType : ObjectGraphType<AppInsights>
    {
        public AppInsightsType()
        {
            Field(x => x.session_Id);
            Field(x => x.user_Id);
            Field(x => x.type);
        }
    }
}
