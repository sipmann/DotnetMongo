using DotnetCrud.Data;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Mongosettings>(options => {
    options.Connection = builder.Configuration["MONGODB_CONNECTION"];
    options.DatabaseName = builder.Configuration["MONGODB_DATABASE"];
    options.IsSSL = Boolean.Parse(builder.Configuration["MONGODB_ISSSL"]);
});

builder.Services.AddScoped<IMongoDbContext, MongoDbContext>();

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.MapGet("/go", async (IMongoDbContext context) => {
    var result = await context.GetCollection("teste").FindAsync(new BsonDocument());
    return await result.ToListAsync();
});

app.MapGet("/goagain", (IMongoDbContext context) => context.GetCollection("teste").Find(new BsonDocument()).ToList<BaseCollection>());

app.MapGet("/goagain2", List<BaseCollection> (IMongoDbContext context) => {
    var lst = context.GetCollection("teste").Find(new BsonDocument()).ToList<BaseCollection>();
    return lst;
});

app.MapGet("/{entity}", async (string entity, IMongoDbContext context) => {
    var result = await context.GetCollection(entity).FindAsync<BsonDocument>(new BsonDocument());
    return await result.ToListAsync();
});

app.Run();
