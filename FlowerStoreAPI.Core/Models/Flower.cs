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

        public int ShopId {get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public double Price { get; set; }
    }
}