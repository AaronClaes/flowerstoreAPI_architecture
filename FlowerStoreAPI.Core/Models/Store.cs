using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FlowerStoreAPI.Models
{
    // Includes all parameters that are available for the flower model.
    public class Store
    {
        //tells the database that the Id is used as the primary key
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Adres { get; set; }

        [Required]
        public string Region { get; set; }

         public IEnumerable<Flower> Flowers { get; set; }

         public int Sale {get; set;}
    }
}