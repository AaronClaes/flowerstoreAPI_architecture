using FlowerStoreAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace FlowerStoreAPI.Data
{
    public class FlowerContext : DbContext
    {
        public FlowerContext(DbContextOptions<FlowerContext> opt) : base(opt)
        {

        }

        public DbSet<Flower> Flowers { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Sale> Sales {get; set;}
    }
}