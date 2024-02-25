using E_LibraryApi.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace E_LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Sign_UPBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sign_InBtn_Click(object sender, RoutedEventArgs e)
        {
            /*if(ModelStateDictionary.IsValid)
            {
                var user = new SignInDto
                {
                    UserName = UserNameText.Text,
                    Password = PasswordBox.Password,
                };
                var response = await client.PostAsJsonAsync("api/SignIn", user);
                if(response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<SignInDto>();
                    if(result != null)
                    {
                        MessageBox.Show("Login Successful");
                        this.Hide();
                        var dashboard = new DashBoardForm();
                        dashboard.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid UserName or Password");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid UserName or Password");
                }*/
            //}

        }

        private void UserNameText_MouseEnter(object sender, MouseEventArgs e)
        {
            if(UserNameText.Text == "UserName")
            {
                UserNameText.Clear();
                
            }
        }
    }
}