using ELibrary.Domain.Models;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;

namespace E_LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for IssueBooks.xaml
    /// </summary>
    public partial class IssueBooks : Window
    {
        private ObservableCollection<BorrowedBookInfo> bookList = new ObservableCollection<BorrowedBookInfo>();
        public IssueBooks()
        {
            InitializeComponent();
            this.DataContext = bookList;
        }

        


        private async void BorrowButton_Click(object sender, RoutedEventArgs e)
        {
            string apiUrl = "http://localhost:5179/api/Book/addborrowedbook";
          

            if (string.IsNullOrEmpty(usernametxtbox.Text) || string.IsNullOrEmpty(booknametxtbox.Text))
            {
                MessageBox.Show("Please Enter the Required Info To Borrow The Book","Missing Required Info",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            string username = usernametxtbox.Text;
            string bookName = booknametxtbox.Text;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string requestUrl = $"{apiUrl}?username={username}&BookName={bookName}";

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.PostAsync(requestUrl, null);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content and deserialize it to BorrowedBookInfo
                        var borrowinfo= await response.Content.ReadAsAsync<BorrowedBookInfo>();
                        if (borrowinfo != null)
                        {
                            bookList.Add(borrowinfo);
                            BorrowBookDataGrid.ItemsSource = bookList;
                            MessageBox.Show("Book Borrowed Successfully", "Borrowed", MessageBoxButton.OK, MessageBoxImage.Information);
                            
                        }
                       /* MessageBox.Show(messageBoxText: "Can't Borrow This Book", "Borrowed", MessageBoxButton.OK, MessageBoxImage.Information);*/
                    }
                    else
                    {
                        MessageBox.Show("Can't Borrow This Book,Not Available", "Borrowed", MessageBoxButton.OK, MessageBoxImage.Information);    
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
