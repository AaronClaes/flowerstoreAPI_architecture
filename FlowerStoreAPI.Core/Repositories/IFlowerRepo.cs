using System.Collections.Generic;
using FlowerStoreAPI.Models;
using FlowerStoreAPI.Data;
using System.Threading.Tasks;

namespace FlowerStoreAPI.Repositories
{   
    public interface IFlowerRepo
    {   
        bool SaveChanges();
        Task<IEnumerable<Flower>> GetAllFlowers();
        Task<Flower> GetFlowerById(int id);
        void CreateFlower(Flower flower);
        void UpdateFlower(Flower flower);
        void DeleteFlower(Flower flower);
    }
}