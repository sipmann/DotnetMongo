using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotnetCrud.Data
{
    public class BaseCollection
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        public BsonDocument Metadata { get; set; }
    }
}