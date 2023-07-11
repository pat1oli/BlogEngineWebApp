using AutoMapper;
using BlogEngineWebApp.Dto;
using BlogEngineWebApp.Models;

namespace BlogEngineWebApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Post, PostDto>();
            CreateMap<PostDto, Post>();
        }
    }
}
