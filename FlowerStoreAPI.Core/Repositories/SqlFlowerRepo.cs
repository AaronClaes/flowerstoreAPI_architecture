using System;
using System.Collections.Generic;
using System.Linq;
using FlowerStoreAPI.Data;
using FlowerStoreAPI.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FlowerStoreAPI.api.Models;
using MongoDB.Driver;

namespace FlowerStoreAPI.Repositories
{
    public class SqlFlowerRepo : IFlowerRepo
    {
        
        private readonly FlowerContext _context;
        public IMongoCollection<Flower> Flowers { get; }
        public MongoClient MongoClient { get; }

           public SqlFlowerRepo(FlowerContext context)
        {
            _context = context;
        }

        
        //function called to create flowers
        public async Task CreateFlower(Flower flower)
        {
            if(flower == null){
                throw new System.NotImplementedException(nameof(flower));
            }
            
            _context.Flowers.Add(flower);
        }


        //function called to delete flowers
        public async Task DeleteFlower(int id)
        {
            var Flower = await GetFlowerById(id);
            if(Flower == null)
            {
                throw new ArgumentNullException(nameof(Flower));
            }
            _context.Flowers.Remove(Flower);
        }


        //function called to get all flowers from database
        public async Task<IEnumerable<Flower>> GetAllFlowers()
        {
            var storeWithFlowers = await _context.Stores
            .Include(x => x.Flowers)
            .FirstOrDefaultAsync(x => x.Id == ShopId);

            if(storeWithFlowers == null){
                throw new NotFoundException();
            }
            return storeWithFlowers.Flowers;
        }


        //function called to get specific flower by id
        public async Task<Flower> GetFlowerById(int id)
        {
            var flower = await _context.Flowers.FirstOrDefaultAsync(x => x.Id == id && x.ShopId == ShopId);
            if(flower == null)
            {
                throw new NotFoundException();
            }
             return flower;
        }


        //function called to save changes to database
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateFlower(Flower flower)
        {
            //nothing
        }

        private async Task CheckStoreExists(int ShopId)
        {
            var shopCheck = await _context.Stores.FindAsync(ShopId);
            if (shopCheck == null)
            {
                throw new NotFoundException();
            }
        }
    }
}