using GraphQL.Types;
using GraphQL;
using GraphQLDemo.Contracts;

namespace GraphQLDemo.GraphQL
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery()
        {
            Field<UserType>(
                "user",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }
                ),
                resolve: context =>
                {
                    var userService = context.RequestServices.GetRequiredService<IUserService>();
                    var id = context.GetArgument<int>("id");

                    return userService.GetUserById(id);
                }
            );

            FieldAsync<ListGraphType<AppInsightsType>>(
                "searchElasticData",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "text" }
                ),
                resolve: async context =>
                {
                    var elastic = context.RequestServices.GetRequiredService<IElasticSearch>();
                    var text = context.GetArgument<string>("text");

                    return await elastic.GetData(text);
                }
            );
        }
    }
}