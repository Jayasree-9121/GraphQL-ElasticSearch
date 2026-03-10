using GraphQL.Types;
using GraphQLDemo.Contracts;
using GraphQL;

namespace GraphQLDemo.GraphQL
{
    public class AppMutation : ObjectGraphType
    {
        public AppMutation(IUserService userService)
        {
            // 🔹 Create User
            Field<UserType>(
                "createUser",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" }
                ),
                resolve: context =>
                {
                    var name = context.GetArgument<string>("name");
                    return userService.CreateUser(name);
                }
            );

            // 🔹 Create Order
            Field<OrderType>(
                "createOrder",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "userId" },
                    new QueryArgument<NonNullGraphType<DecimalGraphType>> { Name = "total" }
                ),
                resolve: context =>
                {
                    var userId = context.GetArgument<int>("userId");
                    var total = context.GetArgument<decimal>("total");

                    return userService.CreateOrder(userId, total);
                }
            );
        }
    }
}
