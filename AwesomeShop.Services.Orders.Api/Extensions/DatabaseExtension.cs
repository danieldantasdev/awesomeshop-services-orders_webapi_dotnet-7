using AwesomeShop.Services.Orders.Infrastructure.Persistence.Repositories.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AwesomeShop.Services.Orders.Api.Extensions;

public static class DatabaseExtension
{
    public static IServiceCollection AddMongoDbExtension(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton(sc =>
        {
            var configuration = sc.GetService<IConfiguration>();
            var options = new MongoDbOption();
            configuration?.GetSection("MongoDb").Bind(options);
            return options;
        });

        serviceCollection.AddSingleton<IMongoClient>(sc =>
        {
            var options = sc.GetService<MongoDbOption>();
            return new MongoClient(options?.ConnectionString);
        });

        serviceCollection.AddTransient(sc =>
        {
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
            var options = sc.GetService<MongoDbOption>();
            var mongoClient = sc.GetService<IMongoClient>();
            return mongoClient?.GetDatabase(options?.Database);
        });

        return serviceCollection;
    }
}