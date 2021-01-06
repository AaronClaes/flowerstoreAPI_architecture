using System.Collections.Generic;
using FlowerStoreAPI.Models;
using FlowerStoreAPI.Data;
using System.Threading.Tasks;

namespace FlowerStoreAPI.Repositories
{   
    public interface IFlowerRepo
    {   
        bool SaveChanges();
        Task<IEnumerable<Flower>> GetAllFlowers(int shopId);
        Task<Flower> GetFlowerById(int shopId, int id);
        void CreateFlower(int shopId, Flower flower);
        void UpdateFlower(int shopId, Flower flower);
        void DeleteFlower(int shopId, int id);
    }
}