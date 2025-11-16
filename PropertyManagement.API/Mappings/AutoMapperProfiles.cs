using AutoMapper;
using PropertyManagement.API.Models.Domain;
using PropertyManagement.API.Models.DTO;

namespace PropertyManagement.API.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            // Property
            CreateMap<Property, PropertyDto>();

            CreateMap<AddPropertyRequestDto, Property>();

            CreateMap<UpdatePropertyRequestDto, Property>();

            // User
            CreateMap<User, UserDto>();

            CreateMap<AddUserRequestDto, User>();

            CreateMap<User, UserDetailsDto>()
                .ForMember(
                    dest => dest.Properties,
                    opt => opt.MapFrom(src => src.Properties));

            CreateMap<Property, UserPropertySummaryDto>();
        }
    }
}