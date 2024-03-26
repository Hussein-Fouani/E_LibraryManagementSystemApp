using AutoMapper;
using ELibrary.Domain.Models;
using ELibrary.Domain.NewFolder;


namespace E_LibraryManagementSystem.Mapper
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<SignUp, SignUPDto>().ReverseMap();
        }
    }
}
