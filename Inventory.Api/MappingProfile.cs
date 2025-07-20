using AutoMapper;
using WareSync.Api.DTOs;
using WareSync.Business;
using WareSync.Domain;

namespace WareSync.Api;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //App Users
        CreateMap<CreateUserDto, AppUser>();
        CreateMap<UserCredentialDto, AppUser>();
        CreateMap<AppUser, CreateUserDto>();
        CreateMap<AppUser, UserDto>()
            .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => src.UserRole.ToString()));
        CreateMap<UserDto, AppUser>()
            .ForMember(dest => dest.UserRole, opt => opt.MapFrom(src => Enum.Parse<UserRole>(src.UserRole)));

        //Customer mappings
        CreateMap<Customer, CustomerDto>();
        CreateMap<CreateCustomerDto, Customer>();
        CreateMap<UpdateCustomerDto, Customer>();

        //Delivery mappings
        CreateMap<Delivery, DeliveryDto>()
            .ForMember(dest => dest.DeliveryDetails, opt => opt.MapFrom(src => src.DeliveryDetails));
        CreateMap<CreateDeliveryDto, Delivery>()
            .ForMember(dest => dest.DeliveryDetails, opt => opt.MapFrom(src => src.DeliveryDetails));

        CreateMap<UpdateDeliveryDto, Delivery>();

        CreateMap<DeliveryDto, Delivery>();

        //DeliveryDetail mappings
        CreateMap<DeliveryDetail, DeliveryDetailDto>()
        .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

        CreateMap<DeliveryDetailDto, DeliveryDetail>();

        CreateMap<CreateDeliveryDetailDto, DeliveryDetail>();
        //Product Mapping
        CreateMap<Product, ProductDto>();
    }
}