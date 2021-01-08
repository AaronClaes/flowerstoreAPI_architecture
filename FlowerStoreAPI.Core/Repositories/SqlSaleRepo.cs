using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlowerStoreAPI.Data;
using FlowerStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowerStoreAPI.Repositories
{

    public class SqlSaleRepo : ISaleRepo
    {

        private readonly FlowerContext _context;
        public SqlSaleRepo(FlowerContext context)
        {
            _context = context;
        }

        public void CreateSale(Sale sale)
        {
            if (sale == null)
            {
                throw new ArgumentNullException(nameof(sale));
            }

            _context.Sales.Add(sale);
        }

        public void DeleteSale(Sale sale)
        {
            if (sale == null)
            {
                throw new ArgumentNullException(nameof(sale));
            }

            _context.Sales.Remove(sale);
        }

        public async Task<IEnumerable<Sale>> GetAllSales()
        {
            return await _context.Sales.ToListAsync();
        }

        public async Task<Sale> GetSaleById(int id)
        {
            return await _context.Sales.FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateSale(Sale sale)
        {

        }
    }
}
