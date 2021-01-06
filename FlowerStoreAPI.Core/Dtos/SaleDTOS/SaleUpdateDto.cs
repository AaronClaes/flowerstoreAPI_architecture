using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FlowerStoreAPI.Models;

namespace FlowerStoreAPI.Dtos.SaleDTOS
{
    //Includes all parameters that is required when doing a POST request. 
    public class SaleUpdateDto
    {   
        [Required]
        public IEnumerable<Flower> Flowers { get; set; }

        [Required]
        public IEnumerable<Store> Stores { get; set; }
        
    }
}