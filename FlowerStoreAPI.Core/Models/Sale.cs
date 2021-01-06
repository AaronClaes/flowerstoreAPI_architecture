using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FlowerStoreAPI.Models
{
    // Includes all parameters that are available for the flower model.
    public class Sale
    {

        //tells the database that the Id is used as the primary key
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }
        
        [BsonRequired]
        public IEnumerable<Flower> Flowers { get; set; }

        [BsonRequired]
        public IEnumerable<Store> Store{ get; set; }
    }
}