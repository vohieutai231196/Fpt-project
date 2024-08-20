using Application.Models;
using AutoMapper;
using Domain;

namespace Application.MappingProfile
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<NewProperty, Property>();
            CreateMap<Property, PropertyDto>();
        }
    }
}
