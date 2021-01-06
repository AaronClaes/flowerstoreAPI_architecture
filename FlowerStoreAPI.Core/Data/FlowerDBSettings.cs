namespace FlowerStoreAPI.Data
{
    public class FlowerDBSettings : IFlowerDBSettings
    {
        public string FlowerCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}