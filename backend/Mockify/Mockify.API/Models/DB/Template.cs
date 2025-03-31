using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Mockify.API.Models.DB
{
    public class Template
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("content")]
        public object Content { get; set; }
    }
}
