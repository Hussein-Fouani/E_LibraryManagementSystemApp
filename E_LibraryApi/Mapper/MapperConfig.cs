using AutoMapper;
using ELibrary.Domain.Models;
using ELibrary.Domain.NewFolder;

namespace E_LibraryApi.Mapper
{
    public class MapperConfig:Profile
    {
        public MapperConfig()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<SignUp,UserRL>().ReverseMap();
            
        }
    }
}
