using System;
using System.Collections.Generic;
using System.Linq;
using FlowerStoreAPI.Data;
using FlowerStoreAPI.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FlowerStoreAPI.Repositories
{
    public class SqlStoreRepo : IStoreRepo
    {
        private readonly FlowerContext _context;
        private int sale = 0;

          public SqlStoreRepo(FlowerContext context)
        {
            _context = context; 
        }
        
        //function called to create stores
        public void CreateStore(Store store)
        {
            if(store == null)
            {
                throw new ArgumentNullException(nameof(store));
            }

            _context.Stores.Add(store);
        }
        //function called to delete stores
        public void DeleteStore(Store store)
        {
            if(store == null)
            {
                throw new ArgumentNullException(nameof(store));
            }

            _context.Stores.Remove(store);
        }
        //function called to get all stores from database
        public async Task<IEnumerable<Store>> GetAllStores()
        {
            return await _context.Stores.ToListAsync();
        }
        //function called to get specific store by id
        public async Task<Store> GetStoreById(int id)
        {
            return await _context.Stores.FirstOrDefaultAsync(p => p.Id == id);
        }

        public void RegisterSale(Store store)
        {
             if(store == null)
            {
                throw new ArgumentNullException(nameof(store));
            }

            sale++;
        }

        //function called to save changes to database
        public bool SaveChanges()
        {
             return (_context.SaveChanges() >= 0);
        }

        public void UpdateStore(Store store)
        {
            //nothing
        }
    }
}