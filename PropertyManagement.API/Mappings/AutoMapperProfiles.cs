using AutoMapper;
using PropertyManagement.API.Models.Domain;
using PropertyManagement.API.Models.DTO;

namespace PropertyManagement.API.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Property, PropertyDto>().ReverseMap();
            CreateMap<AddPropertyRequestDto, Property>().ReverseMap();
            CreateMap<UpdatePropertyRequestDto, Property>().ReverseMap();
        }
    }
}
