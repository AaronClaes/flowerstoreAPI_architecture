using System.ComponentModel.DataAnnotations;

namespace FlowerStoreAPI.Dtos.StoreDTOS
{
    //Includes all parameters that is required when doing a POST request. 
    public class StoreCreateDto
    {   
        [Required]
        public string Name { get; set; }

        [Required]
        public string Adres { get; set; }

        [Required]
        public string Region { get; set; }
    }
}