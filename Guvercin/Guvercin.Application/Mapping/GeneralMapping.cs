
using AutoMapper;
using Guvercin.Application.Dtos.AdvertItemsDtos;
using Guvercin.Application.Dtos.CategoryDtos;
using Guvercin.Application.Dtos.UserDtos;
using Guvercin.Domain.Entities;

namespace Guvercin.Application.Mapping;

public class GeneralMapping:Profile
{
    public GeneralMapping()
    {
        CreateMap<Category,CreateCategoryDto>().ReverseMap();
        CreateMap<Category,UpdateCategoryDto>().ReverseMap();
        CreateMap<Category,DetailCategoryDto>().ReverseMap();
        CreateMap<Category,ResultCategoryDto>().ReverseMap();
        CreateMap<Category,ResultCategoriesWithAdvertDto>().ReverseMap();
        
        CreateMap<AdvertItem,CreateAdvertItemDto>().ReverseMap();
        CreateMap<AdvertItem,UpdateAdvertItemDto>().ReverseMap();
        CreateMap<AdvertItem,DetailAdvertItemDto>().ReverseMap();
        CreateMap<AdvertItem,ResultAdvertItemDto>().ReverseMap();
        CreateMap<AdvertItem,CategoriesAdvertItemDto>().ReverseMap();
        
        
    }
    
}