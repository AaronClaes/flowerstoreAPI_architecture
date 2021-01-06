using System.Collections.Generic;
using FlowerStoreAPI.Models;
using FlowerStoreAPI.Data;
using System.Threading.Tasks;

namespace FlowerStoreAPI.Repositories
{
    public interface ISaleRepo
    {
        bool SaveChanges();
        Task<IEnumerable<Sale>> GetAllSales();
        Task<Sale> GetSaleById(int id);
        void CreateSale(Sale sale);
        void UpdateSale(Sale sale);
        void DeleteSale(Sale sale);
    }
}