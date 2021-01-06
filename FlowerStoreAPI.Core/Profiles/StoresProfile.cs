using AutoMapper;
using FlowerStoreAPI.Dtos.StoreDTOS;
using FlowerStoreAPI.Models;

namespace FlowerStoreAPI.Profiles
{
    public class StoresProfile : Profile
    {
        public StoresProfile()
        {
            CreateMap<Store, StoreReadDto>(); 
            CreateMap<StoreCreateDto, Store>();
            CreateMap<StoreUpdateDto,Store>();
            CreateMap<Store, StoreUpdateDto>();
            //CreateMap<Store, SaleUpdateDto>();
        }
    }
}