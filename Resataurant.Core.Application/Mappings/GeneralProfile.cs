using AutoMapper;
using Restaurant.Core.Application.Dto.Ingridients;
using Restaurant.Core.Application.Dto.Orders;
using Restaurant.Core.Application.Dto.Plates;
using Restaurant.Core.Application.Dto.Tables;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region Ingridients
            CreateMap<IngridientsDto, Ingridients>()
                .ForMember(i => i.PlatesIngridients, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<IngridientsAddDto, Ingridients>()
                .ForMember(i => i.PlatesIngridients, opt => opt.Ignore())
                .ReverseMap();
            #endregion

            #region Plates
            CreateMap<PlateDto, Plates>()
                .ForMember(p => p.PlatesIngridients, opt => opt.Ignore())
                .ForMember(p => p.PlatesCategories, opt => opt.Ignore())
                .ForMember(p => p.PlatesIngridients, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(p => p.PlateIngridients, opt => opt.Ignore())
                .ForMember(p => p.PlateCategory, opt => opt.Ignore());

            CreateMap<PlatesAddDto, Plates>()
                .ForMember(p => p.PlatesIngridients, opt => opt.Ignore())
                .ForMember(p => p.PlatesCategories, opt => opt.Ignore())
                .ForMember(p => p.PlatesIngridients, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(p => p.PlateIngridients, opt => opt.Ignore());
            #endregion

            #region Tables
            CreateMap<TablesBaseDto, Tables>()
                .ForMember(t => t.Orders, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(t => t.Orders, opt => opt.Ignore());
            #endregion

            #region Orders
            CreateMap<OrdersDto, Orders>()
                .ForMember(o => o.PlateOrders, opt => opt.Ignore())
                .ForMember(o => o.TableId, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<OrderAddDto, Orders>()
                .ForMember(o => o.PlateOrders, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(o => o.PlateOrders, opt => opt.Ignore());
            #endregion
        }
    }
}
