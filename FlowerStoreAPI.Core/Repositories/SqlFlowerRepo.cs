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
        public void CreateFlower(Flower flower)
        {
            if (flower == null)
            {
                throw new System.NotImplementedException(nameof(flower));
            }

            _context.Flowers.Add(flower);
        }


        //function called to delete flowers
        public void DeleteFlower(Flower flower)
        {
            if(flower == null)
            {
                throw new ArgumentNullException(nameof(Flower));
            }
            _context.Flowers.Remove(flower);
        }


        //function called to get all flowers from database
        public async Task<IEnumerable<Flower>> GetAllFlowers()
        {
            return await _context.Flowers.ToListAsync();
        }


        //function called to get specific flower by id
        public async Task<Flower> GetFlowerById(int id)
        {
            return await _context.Flowers.FirstOrDefaultAsync(p => p.Id == id);
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