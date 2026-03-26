using GraphQL;
using GraphQL.Types;
using GraphQLDemo.Contracts;
using GraphQLDemo.Models;
using GraphQLDemo.Pipeline;
using System.Text.Json;

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

            FieldAsync<AIResultType>(
                "analyzeErrorWithAI",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "message" }
                ),
                resolve: async context =>
                {
                    var pipeline = context.RequestServices.GetRequiredService<ErrorPipeline>();

                    var log = new ErrorLog
                    {
                        Message = context.GetArgument<string>("message")
                    };

                    return await pipeline.Run(log);
                }
            );
        }
    }
}