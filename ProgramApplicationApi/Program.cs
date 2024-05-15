using Microsoft.Azure.Cosmos;
using ProgramApplicationApi.Interfaces;
using ProgramApplicationApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddTransient<IProgramRepository, ProgramRepository>();
builder.Services.AddTransient<IApplicationRepository, ApplicationRepository>();
var configuration = builder.Configuration;

builder.Services.AddSingleton((provider) =>
{
    string endpointUrl = configuration["CosmosDb:EndpointUrl"];
    string primaryKey = configuration["CosmosDb:PrimaryKey"];
    string databaseName = configuration["CosmosDb:DatabaseName"];

    var CosmosClientOptions = new CosmosClientOptions
    {
        ApplicationName = databaseName
    };

    LoggerFactory.Create(builder => builder.AddConsole());

    var CosmosClient = new CosmosClient(endpointUrl, primaryKey, CosmosClientOptions);
    CosmosClient.ClientOptions.ConnectionMode = ConnectionMode.Direct;
    return CosmosClient;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
