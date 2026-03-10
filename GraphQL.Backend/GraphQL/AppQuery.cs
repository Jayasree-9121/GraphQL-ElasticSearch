using GraphQL.Types;
using GraphQL;
using GraphQLDemo.Contracts;

namespace GraphQLDemo.GraphQL
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(IUserService userService)
        {
            Field<UserType>(
                "user",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return userService.GetUserById(id);
                }
            );
        }
    }
}
    