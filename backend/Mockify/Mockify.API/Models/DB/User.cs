using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Mockify.API.Models.DB
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("token")]
        public string Token { get; set; }
        [BsonElement("lastLogin")]
        public DateTime LastLogin { get; set; }
        
        [BsonElement("templates")]
        public Template[] Templates { get; set; } = new Template[0];
    }
}
