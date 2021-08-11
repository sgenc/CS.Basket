using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CicekSepeti.Basket.Core.DataModel
{
    public abstract class MongoDbEntity : IEntity<string>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; } = ObjectId.GenerateNewId().ToString();
    }
}