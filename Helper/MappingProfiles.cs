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
            CreateMap<Post, PostDto>();
        }
    }
}
