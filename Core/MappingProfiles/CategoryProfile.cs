using AutoMapper;
using Core.Entities;
using Core.EntitiesDTOs;

namespace Core.MappingProfiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Category, CategoryDetailDto>().ReverseMap();
    }
}
