using AutoMapper;
using E_LibraryManagementSystem.Models;
using E_LibraryManagementSystem.Models.Dto;


namespace E_LibraryManagementSystem.Mapper
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<BookModel, BookDto>().ReverseMap();
            CreateMap<SignInModel,SignInDto>().ReverseMap();
            CreateMap<Student, StudentDto>().ReverseMap();
        }
    }
}
