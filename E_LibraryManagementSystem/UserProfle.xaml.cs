using ELibrary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
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
using ELibrary.Domain.NewFolder;
using Newtonsoft.Json;
using E_LibraryApi.Migrations;

namespace E_LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for UserProfile.xaml
    /// </summary>
    public partial class UserProfile : Window
    {
        private ObservableCollection<BorrowedBookInfo> BorrowedBooks = new ObservableCollection<BorrowedBookInfo>();
        public UserProfile()
        {
            InitializeComponent();
            this.DataContext = BorrowedBooks;
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string apiUrl = "http://localhost:5179/api/Book/";

            if (string.IsNullOrEmpty(UsernameTextbox.Text))
            {
                MessageBox.Show("Please Enter UserName", "Missing Required Info", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string username = UsernameTextbox.Text;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string requestUrl = $"{apiUrl}{username}";

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetStringAsync($"{requestUrl}");
                    var books = JsonConvert.DeserializeObject<List<BorrowedBookInfo>>(response);

                    if (response != null)
                    {
                        if (books != null)
                        {
                            foreach (var book in books)
                            {
                                BorrowedBooks.Add(book);

                            }
                            MessageBox.Show("User Info Successfully Retrieved", caption: "User Info", MessageBoxButton.OK, MessageBoxImage.Information);

                            if (BorrowedBooks.Count == 0)
                            {
                                MessageBox.Show("No Books Found for the User", "User Info", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No Books Found for the User", "User Info", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Can't Retrieve User Info. API Response is null.", "User Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("HTTP Request Error. Check the API URL or network connection.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}

