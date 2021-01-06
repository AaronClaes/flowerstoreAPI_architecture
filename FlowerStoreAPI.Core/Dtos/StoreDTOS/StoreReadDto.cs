using System.ComponentModel.DataAnnotations;

namespace FlowerStoreAPI.Dtos.StoreDTOS
{
    // Includes all parameters that will be shown with GET request. This is useful when some data is not supposed to be available.
    public class StoreReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Adres { get; set; }

        public string Region { get; set; }
    }
}