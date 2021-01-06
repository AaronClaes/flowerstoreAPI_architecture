namespace FlowerStoreAPI.Controllers
{
    // You can use any format of string; however this one is "nice" because if you later want to move
    // to redis you can use the same ones and have implicit namespaces
    public static class CacheKeys
    {
        public static string AllFlowers = "Cache:Flowers:All";
        public static string AllStores = "Cache:Stores:All";
        public static string AllSales = "Cache:Stores:All";
    }
}