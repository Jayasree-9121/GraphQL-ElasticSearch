//using GraphQL;
//using GraphQL.Server.Ui.Playground;
//using GraphQL.Types;
//using GraphQLDemo.Contracts;
//using GraphQLDemo.GraphQL;
//using GraphQLDemo.Pipeline;
//using GraphQLDemo.Serivice;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddHttpClient();

//// Application Services (Scoped)
//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IElasticSearch, ElasticSearch>();


//// GraphQL Types (Singleton)
//builder.Services.AddSingleton<UserType>();
//builder.Services.AddSingleton<OrderType>();
//builder.Services.AddSingleton<ProductType>();
//builder.Services.AddSingleton<AppInsightsType>();


//builder.Services.AddScoped<ILLMService, LLMService>();
//builder.Services.AddScoped<PlanningAgentService>();
//builder.Services.AddScoped<ErrorAnalysisService>();
//builder.Services.AddScoped<ErrorPipeline>();

//// GraphQL Query & Mutation (Singleton)
//builder.Services.AddSingleton<AppQuery>();
//builder.Services.AddSingleton<AppMutation>();


//// GraphQL Schema (Singleton)
//builder.Services.AddSingleton<ISchema, AppSchema>();


//// CORS
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", policy =>
//    {
//        policy.AllowAnyOrigin()
//              .AllowAnyHeader()
//              .AllowAnyMethod();
//    });
//});


//// GraphQL Configuration
//builder.Services.AddGraphQL(builder =>
//{
//    builder
//        .AddSystemTextJson()
//        .AddSchema<AppSchema>()
//        .AddErrorInfoProvider(opt =>
//        {
//            opt.ExposeExceptionDetails = true;
//        });
//});

//var app = builder.Build();


//// Middleware
//app.UseCors("AllowAll");


//// GraphQL Endpoint
//app.UseGraphQL<ISchema>("/graphql");


//// GraphQL Playground
//app.UseGraphQLPlayground("/playground");


//app.Run();





using GraphQL;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using GraphQLDemo.Contracts;
using GraphQLDemo.GraphQL;
using GraphQLDemo.Pipeline;
using GraphQLDemo.Serivice;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

// Existing Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IElasticSearch, ElasticSearch>();

// ✅ AI Services (IMPORTANT)
builder.Services.AddScoped<ILLMService, LLMService>();
builder.Services.AddScoped<PlanningAgentService>();
builder.Services.AddScoped<ErrorAnalysisService>();
builder.Services.AddScoped<ErrorPipeline>();
builder.Services.AddSingleton<PlanningResultType>();
builder.Services.AddSingleton<ErrorAnalysisResultType>();
builder.Services.AddSingleton<AIResultType>();

// GraphQL Types
builder.Services.AddSingleton<UserType>();
builder.Services.AddSingleton<OrderType>();
builder.Services.AddSingleton<ProductType>();
builder.Services.AddSingleton<AppInsightsType>();

// GraphQL Query & Mutation
builder.Services.AddSingleton<AppQuery>();
builder.Services.AddSingleton<AppMutation>();

// Schema
builder.Services.AddSingleton<ISchema, AppSchema>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// GraphQL
builder.Services.AddGraphQL(builder =>
{
    builder
        .AddSystemTextJson()
        .AddSchema<AppSchema>()
        .AddErrorInfoProvider(opt =>
        {
            opt.ExposeExceptionDetails = true;
        });
});

var app = builder.Build();

app.UseCors("AllowAll");

app.UseGraphQL<ISchema>("/graphql");
app.UseGraphQLPlayground("/playground");

app.Run();