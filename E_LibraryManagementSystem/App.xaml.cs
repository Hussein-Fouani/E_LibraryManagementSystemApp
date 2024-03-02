using AutoMapper;
using E_LibraryApi.Mapper;
using E_LibraryManagementSystem.Mapper;
using System.Configuration;
using System.Data;
using System.Windows;

namespace E_LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
       private static MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MapperConfig>();
            cfg.AddProfile<MappingConfig>(); 
        });

      public static  IMapper mapper = mapperConfiguration.CreateMapper();

    }

}
