using System.ComponentModel.DataAnnotations;

namespace FlowerStoreAPI.Dtos.StoreDTOS
{
    //Includes all parameters that is required when doing a PUT request. 
    public class StoreUpdateDto
    {   
        [Required]
        public string Name { get; set; }

        [Required]
        public string Adres { get; set; }

        [Required]
        public string Region { get; set; }
    }
}