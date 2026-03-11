using GraphQL;
using GraphQL.Types;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQLDemo.Contracts;
using GraphQLDemo.GraphQL;
using GraphQLDemo.Serivice;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddSingleton<IUserService, UserService>();

// Register GraphQL types
builder.Services.AddSingleton<UserType>();
builder.Services.AddSingleton<OrderType>();
builder.Services.AddSingleton<ProductType>();
builder.Services.AddSingleton<AppQuery>();
builder.Services.AddSingleton<ISchema, AppSchema>();
builder.Services.AddSingleton<AppMutation>();
builder.Services.AddScoped<IElasticSearch,ElasticSearch>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// ✅ New Correct Way (No EnableMetrics)
builder.Services.AddGraphQL(builder =>
{
    builder
        .AddSystemTextJson()
        .AddSchema<AppSchema>();
});

var app = builder.Build();

app.UseCors("AllowAll");

// Single endpoint
app.UseGraphQL<ISchema>("/graphql");

// Playground UI
app.UseGraphQLPlayground("/playground");

app.Run();