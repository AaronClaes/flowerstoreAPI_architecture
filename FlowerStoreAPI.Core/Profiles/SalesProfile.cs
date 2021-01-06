using AutoMapper;
using FlowerStoreAPI.Dtos;
using FlowerStoreAPI.Dtos.FlowerDTOS;
using FlowerStoreAPI.Dtos.SaleDTOS;
using FlowerStoreAPI.Models;

namespace FlowerStoreAPI.Profiles
{
    public class SalesProfile : Profile
    {
        public SalesProfile()
        {
            CreateMap<Sale, SaleReadDto>(); 
            CreateMap<SaleCreateDto, Sale>();
            CreateMap<SaleUpdateDto,Sale>();
            CreateMap<Sale, SaleUpdateDto>();
        }
    }
}