namespace FlowerStoreAPI.Data
{
    // 1:1 from appsettings.Development.json
    public interface IFlowerDBSettings
    {
        string FlowerCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}