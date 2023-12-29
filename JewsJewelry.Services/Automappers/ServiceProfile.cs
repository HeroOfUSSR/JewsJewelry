using AutoMapper;
using JewsJewelry.Context.Contracts.Models;
using JewsJewelry.Services.Contracts.Models;
using JewsJewelry.Services.Contracts.ModelsRequest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services.Automappers
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Craftsman, CraftsmanModel>(MemberList.Destination).ReverseMap();
            CreateMap<Customer, CustomerModel>(MemberList.Destination).ReverseMap();
            CreateMap<Jewelry, JewelryModel>(MemberList.Destination).ReverseMap();
            CreateMap<Material, MaterialModel>(MemberList.Destination).ReverseMap();
            CreateMap<Workshop, WorkshopModel>(MemberList.Destination).ReverseMap();
            CreateMap<Order, OrderModel>(MemberList.Destination)
                .ForMember(o => o.Jewelry, opt => opt.Ignore())
                .ForMember(o => o.Customer, opt => opt.Ignore())
                .ForMember(o => o.Workshop, opt => opt.Ignore()).ReverseMap();

            CreateMap<OrderRequestModel, Order>(MemberList.Destination)
                .ForMember(o => o.Jewelries, opt => opt.Ignore())
                .ForMember(o => o.Customer, opt => opt.Ignore())
                .ForMember(o => o.Workshop, opt => opt.Ignore())
                .ForMember(o => o.CreatedAt, opt => opt.Ignore())
                .ForMember(o => o.DeletedAt, opt => opt.Ignore())
                .ForMember(o => o.CreatedBy, opt => opt.Ignore())
                .ForMember(o => o.UpdatedAt, opt => opt.Ignore())
                .ForMember(o => o.UpdatedBy, opt => opt.Ignore());
        }
    }
}
