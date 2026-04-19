using LibraryPlus.Endpoints;
using LibraryPlus.Extensions;
using LibraryPlus.Services;
using LibraryPlus.Services.Auth;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

var builder = WebApplication.CreateBuilder(args);

var pack = new ConventionPack { new CamelCaseElementNameConvention() };
ConventionRegistry.Register("camel case", pack, t => true);
var connectionString = builder.Configuration.GetSection("MongoDbSettings:ConnectionString").Value;

builder.Services.AddSingleton<IMongoClient>(new MongoClient(connectionString));
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<JwtService>();
builder.Services.AddSingleton<RefreshTokenService>();
builder.Services.AddSingleton<AuthService>();

builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapAuthEndpoints();

app.Run();
