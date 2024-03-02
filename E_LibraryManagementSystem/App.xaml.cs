using AutoMapper;
using E_LibraryApi.Mapper;
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
        MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MapperConfig>();
        });
        IMapper mapper = mapperConfiguration.CreateMapper();
    }

}
