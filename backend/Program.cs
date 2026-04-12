using LibraryPlus.Endpoints;
using LibraryPlus.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetSection("MongoDbSettings:ConnectionString").Value;

builder.Services.AddSingleton<IMongoClient>(new MongoClient(connectionString));
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<AuthService>();

var app = builder.Build();

app.MapUserEndpoints();

app.Run();
