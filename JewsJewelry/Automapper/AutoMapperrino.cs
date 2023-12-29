using AutoMapper;
using JewsJewelry.API.Models.CreateRequest;
using JewsJewelry.API.Models.Request;
using JewsJewelry.API.Models.Response;
using JewsJewelry.Services.Contracts.Models;
using JewsJewelry.Services.Contracts.ModelsRequest;

namespace JewsJewelry.API.Automapper
{
    public class AutoMapperrino : Profile
    {
        public AutoMapperrino()
        {
            CreateMap<CreateCraftsmanReq, CraftsmanModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<CreateCustomerReq, CustomerModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<CreateJewelryReq, JewelryModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<CreateMaterialReq, MaterialModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<CreateWorkshopReq, WorkshopModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<CraftsmanRequest, CraftsmanModel>(MemberList.Destination).ReverseMap();
            CreateMap<CustomerRequest, CustomerModel>(MemberList.Destination).ReverseMap();
            CreateMap<JewelryRequest, JewelryModel>(MemberList.Destination).ReverseMap();
            CreateMap<MaterialRequest, MaterialModel>(MemberList.Destination).ReverseMap();
            CreateMap<WorkshopRequest, WorkshopModel>(MemberList.Destination).ReverseMap();
            CreateMap<OrderRequest, OrderModel>(MemberList.Destination)
                .ForMember(x => x.Jewelry, opt => opt.Ignore())
                .ForMember(x => x.Workshop, opt => opt.Ignore())
                .ForMember(x => x.Customer, opt => opt.Ignore()).ReverseMap();

            CreateMap<OrderRequest, OrderRequestModel>(MemberList.Destination);
            CreateMap<CreateOrderReq, OrderRequestModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<JewelryModel, JewelryResponse>(MemberList.Destination);
            CreateMap<WorkshopModel, WorkshopResponse>(MemberList.Destination);
            CreateMap<MaterialModel, MaterialResponse>(MemberList.Destination);
            CreateMap<OrderModel, OrderResponse>(MemberList.Destination);
            CreateMap<CustomerModel, CustomerResponse>(MemberList.Destination)
                .ForMember(x => x.Name, opt => opt.MapFrom(src => $"{src.Name} {src.Surname} {src.Patronymic}"));

            CreateMap<CraftsmanModel, CraftsmanResponse>(MemberList.Destination)
                .ForMember(x => x.Name, opt => opt.MapFrom(src => $"{src.Name} {src.Surname} {src.Patronymic}"));
        }
    }
}
