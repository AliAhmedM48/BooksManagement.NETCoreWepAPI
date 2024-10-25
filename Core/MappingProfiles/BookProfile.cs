using AutoMapper;
using Core.Entities;
using Core.EntitiesDTOs;

namespace Core.MappingProfiles;
public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookDto>().ReverseMap();

        CreateMap<Book, BookDetailDto>()
       .ForMember(p => p.CategoryName, options => options.MapFrom(s => s.Category.Name))
       .ReverseMap();
    }
}
