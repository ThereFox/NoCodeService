using NoCode.Application;
using NoCodeConstructor.Domain.Actions;
using NoCodeConstructor.Domain.Triggers;
using NoCodeConstructor.Nodes;
using NoCodeConstructor.Persistense;
using NodeBuilder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

builder.Services
    .AddNodeRegistration()
    .AddApplication()
    .AddPersistence();

builder.Services.AddMvcCore();

var app = builder.Build();

app.MapControllers();
app.Services.AddCommonNodes();

app.Run();