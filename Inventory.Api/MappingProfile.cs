using AutoMapper;
using WareSync.Api.DTOs;
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


    }
} 