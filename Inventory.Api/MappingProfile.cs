using AutoMapper;
using WareSync.Api.DTOs;
using WareSync.Domain;
using WareSync.Business;

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
        CreateMap<Delivery, DeliveryDto>();
        CreateMap<CreateDeliveryDto, Delivery>()
            .ForMember(dest => dest.DeliveryDetails, opt => opt.MapFrom(src => src.DeliveryDetails));

        CreateMap<DeliveryDto, Delivery>();

        //DeliveryDetail mappings
        CreateMap<DeliveryDetail, DeliveryDetailDto>();
        CreateMap<DeliveryDetailDto, DeliveryDetail>()
     .ForMember(dest => dest.Delivery, opt => opt.Ignore());

        CreateMap<CreateDeliveryDetailDto, DeliveryDetail>();
    }
} 