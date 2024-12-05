using MetaCortex.Products.API.Endpoints;
using MetaCortex.Products.API.Services.Interfaces;
using MetaCortex.Products.DataAccess;
using MetaCortex.Products.DataAccess.Interface;
using MetaCortex.Products.DataAccess.MongoDb;
using MetaCortex.Products.DataAccess.RabbitMq;
using MetaCortex.Products.DataAccess.Repository;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MetaCortex.Products.API.BackgroundServices;
using MetaCortex.Products.API.Services.RabbitMqServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient($"mongodb://{settings.Host}:{settings.Port}");
});

builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.Configure<RabbitMqConfiguration>(builder.Configuration.GetSection("RabbitMqConfiguration"));
builder.Services.AddSingleton<RabbitMqConfiguration>(sp =>
    sp.GetRequiredService<IOptions<RabbitMqConfiguration>>().Value);

builder.Services.AddSingleton<IRabbitMqService, RabbitMqService>();
builder.Services.AddSingleton<IMessageProducerService, MessageProducerService>();
builder.Services.AddSingleton<IMessageConsumerService, MessageConsumerService>();
builder.Services.AddHostedService<MessageConsumerHostedService>();
var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World!");
app.MapProductEndpoints();

app.Run();
