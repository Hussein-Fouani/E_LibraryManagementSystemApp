using E_LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace E_LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void clickmeimage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenGitHubRepository();
        }
        private void OpenGitHubRepository()
        {
            string githubRepository = "https://github.com/Hussein-Fouani/E_LibraryManagementSystemApp";
            try
            {
                Process.Start(new ProcessStartInfo(githubRepository));

            }catch(Exception f)
            {
                MessageBox.Show($"Error Opening GitHub Account ","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }

            
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            string username = SignUpUserNamebox.Text;
            string password = SignUpPasswordbox.Password;
            if(username.Length < 4 || password.Length < 8)
            {
                MessageBox.Show("Username must be at least 4 characters and password must be at least 8 characters","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            SignUpModel signUpModel = new SignUpModel()
            {
                UserName = username,
                Password = password
            };


        }
    }
}
