using AgroCare.Data.DTOs;
using AutoMapper;
using Models.Models;

namespace AgroCare.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<Models.Models.Action, ActionDto>().ReverseMap();

            CreateMap<AgriculturalItem, AgriculturalItemDto>().ReverseMap();

            CreateMap<Buyer, BuyerDto>().ReverseMap();

            CreateMap<Engineer, EngineerDto>().ReverseMap();

            CreateMap<EngineerType, EngineerTypeDto>().ReverseMap();

            CreateMap<Farmer, FarmerDto>().ReverseMap();

            CreateMap<Item, ItemDto>().ReverseMap();

            CreateMap<Land, LandDto>().ReverseMap();

            CreateMap<LandType, LandTypeDto>().ReverseMap();

            CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();

            CreateMap<Order, OrderDto>().ReverseMap();

            CreateMap<Plan, PlanDto>().ReverseMap();

            CreateMap<PurchaseDetail, PurchaseDetailDto>().ReverseMap();

            CreateMap<Purchase, PurchaseDto>().ReverseMap();

            CreateMap<SoilType, SoilTypeDto>().ReverseMap();

            CreateMap<StepDetail, StepDetailDto>().ReverseMap();

            CreateMap<Step, StepDto>().ReverseMap();

            CreateMap<Store, StoreDto>().ReverseMap();

            CreateMap<StoreType, StoreTypeDto>().ReverseMap();
        }
    }
}
