using AutoMapper;
using E_LibraryApi.Mapper;
using E_LibraryManagementSystem.Mapper;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace E_LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
            protected override void OnStartup(StartupEventArgs e)
            {
                Window window = new MainWindow();
                window.Show();
                base.OnStartup(e);
            }


          
    }
    

}
