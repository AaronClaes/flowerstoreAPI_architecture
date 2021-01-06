using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FlowerStoreAPI.Models;

namespace FlowerStoreAPI.Dtos.SaleDTOS
{
    //Includes all parameters that is required when doing a POST request. 
    public class SaleReadDto
    {   
        public IEnumerable<Flower> Flowers { get; set; }
        
        public IEnumerable<Store> Stores { get; set; }
        
    }
}