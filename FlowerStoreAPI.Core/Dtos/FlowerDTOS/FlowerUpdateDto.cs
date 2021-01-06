using System.ComponentModel.DataAnnotations;

namespace FlowerStoreAPI.Dtos.FlowerDTOS
{
    //Includes all parameters that is required when doing a PUT request. 
    public class FlowerUpdateDto
    {      
        [Required]
        public string Name { get; set; }
       
        [Required]
        public string Color { get; set; }

        [Required]
        public double Price { get; set; }
        
    }
}