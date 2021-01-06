using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FlowerStoreAPI.Models
{
    // Includes all parameters that are available for the flower model.
    public abstract class Flower
    {

        //tells the database that the Id is used as the primary key
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }

        [BsonRequired]
        public string Name { get; set; }

        [BsonRequired]
        public string Color { get; set; }

        [BsonRequired]
        public double Price { get; set; }
    }
}