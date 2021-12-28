using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DotnetCrud.Data
{
    public interface IMongoDbContext
    {
        IMongoCollection<BaseCollection> GetCollection(string tableName);
    }

    public class Mongosettings
    {
        public string Connection { get; set; }
        public string DatabaseName { get; set; }
        public bool IsSSL { get; set; }
    }

    public class MongoDbContext : IMongoDbContext
    {
        private IMongoDatabase _database { get; }

        public MongoDbContext(IOptions<Mongosettings> configuration)
        {
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(configuration.Value.Connection));
                if (configuration.Value.IsSSL)
                {
                    settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                }
                var mongoClient = new MongoClient(settings);
                _database = mongoClient.GetDatabase(configuration.Value.DatabaseName);

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível se conectar com o servidor.", ex);
            }
        }

        public IMongoCollection<BaseCollection> GetCollection(string tableName)
            => _database.GetCollection<BaseCollection>(tableName);

        /*public IMongoCollection<Nota> Notas
        {
            get
            {
                return _database.GetCollection<Nota>("Notas");
            }
        }*/
    }
}