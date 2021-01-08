using System.Collections.Generic;
using FlowerStoreAPI.Models;
using FlowerStoreAPI.Data;
using System.Threading.Tasks;

namespace FlowerStoreAPI.Repositories
{
    public interface IStoreRepo
    {
        bool SaveChanges();
        Task<IEnumerable<Store>> GetAllStores();
        Task<Store> GetStoreById(int id);
        void CreateStore(Store store);
        void UpdateStore(Store store);
        void DeleteStore(Store store);
    }
}