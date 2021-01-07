using System.Collections.Generic;
using System.Threading.Tasks;
using FlowerStoreAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;

public class MongoDbService
            {
                private IMongoCollection<Sale> SaleCollection{get; }
                public MongoDbService(string DatabaseName, string SaleCollectionName, string ConnectionString)
                {
                    var mongoClient = new MongoClient(ConnectionString);
                    var mongoDatabase = mongoClient.GetDatabase(DatabaseName);

                    SaleCollection = mongoDatabase.GetCollection<Sale>(SaleCollectionName);
                }
               
                public async Task InsertSale(Sale sale) => await SaleCollection.InsertOneAsync(sale);
                
                public async Task<List<Sale>> GetAllSales()
                {
                    var sales = new List<Sale>();

                    var allDocuments = await SaleCollection.FindAsync(new BsonDocument());
                    await allDocuments.ForEachAsync(doc => sales.Add(doc));

                    return sales;
                }
            }