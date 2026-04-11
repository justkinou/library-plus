using MongoDB.Bson;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetSection("MongoDbSettings:ConnectionString").Value;
var databaseName = builder.Configuration.GetSection("MongoDbSettings:DatabaseName").Value;

builder.Services.AddSingleton<IMongoClient>(new MongoClient(connectionString));

var app = builder.Build();

app.Run();
