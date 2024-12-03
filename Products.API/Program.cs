using MetaCortex.Products.API.Endpoints;
using MetaCortex.Products.DataAccess;
using MetaCortex.Products.DataAccess.Interface;
using MetaCortex.Products.DataAccess.MongoDb;
using MetaCortex.Products.DataAccess.Repository;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient($"mongodb://{settings.Host}:{settings.Port}");
});

builder.Services.AddSingleton<IProductRepository, ProductRepository>();
var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World!");
app.MapProductEndpoints();

app.Run();
