using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using NoCode.Application;
using NoCodeConstructor.Domain.Actions;
using NoCodeConstructor.Domain.Triggers;
using NoCodeConstructor.Nodes;
using NoCodeConstructor.Persistense;
using NodeBuilder;
using Persistense.EFCore;
using WebAPI.Configuration;

var builder = WebApplication.CreateBuilder(args);

var servicesConfiguration = builder
    .Configuration
    .GetSection("Services").Get<ServicesConfiguration>();

builder.Services.AddHttpClient();

builder.Services.AddMemoryCache();

builder.Services
    .AddNodeRegistration()
    .AddApplication()
    .AddMongoDBPipelineStore(servicesConfiguration.MongoDB.ConnectionString)
    .AddEFCoreKeyValue(
        ex => ex.UseNpgsql(servicesConfiguration.PostgreSQL.ConnectionString)
            .UseExceptionProcessor()
        );

builder.Services.AddMvcCore();

var app = builder.Build();

app.MapControllers();
app.Services.AddCommonNodes();

app.Run();