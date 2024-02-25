using AutoMapper;
using E_LibraryManagementSystem.Models;

namespace E_LibraryApi.Mapper
{
    public class MapperConfig:Profile
    {
        public MapperConfig()
        {
            CreateMap<Models.BookModel, Models.Dto.BookDto>().ReverseMap();
            CreateMap<SignInModel, Models.Dto.SignInDto>().ReverseMap();
        }
    }
}
