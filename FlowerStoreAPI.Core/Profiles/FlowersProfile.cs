using AutoMapper;
using FlowerStoreAPI.Dtos;
using FlowerStoreAPI.Dtos.FlowerDTOS;
using FlowerStoreAPI.Models;

namespace FlowerStoreAPI.Profiles
{
    public class FlowersProfile : Profile
    {
        public FlowersProfile()
        {
            CreateMap<Flower, FlowerReadDto>(); 
            CreateMap<FlowerCreateDto, Flower>();
            CreateMap<FlowerUpdateDto,Flower>();
            CreateMap<Flower, FlowerUpdateDto>();
        }
    }
}