using ELibrary.Domain.NewFolder;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

using MessageBox = System.Windows.MessageBox;
using TextBox = System.Windows.Controls.TextBox;

namespace E_LibraryManagementSystem
{

    public partial class AddBookForm : Window
    {
        HttpClient client = new HttpClient();

        public AddBookForm()
        {
            InitializeComponent();
            client.BaseAddress = new Uri("https://localhost:44360/api/Book");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        }


        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(GetWindow(this), "Do you want to close this window?", "Close Window", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var book in BookSubForm.Children)
                {
                    if (book is TextBox box)
                    {
                        box.Clear();
                    }
                    else if (book is DatePicker picker)
                    {
                        picker.SelectedDate = null;
                    }
                }
                this.UpdateLayout();
                this.Close();
            }
        }


        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AreTextBoxesFilled())
            {
                try
                {
                    if (double.TryParse(Book_Price.Text, out double price))
                    {
                        var bookModel = new BookDto
                        {
                            BookName = txtBookName.Text,
                            BookAuthor = BookAuthor.Text,
                            BookPrice = price,
                            ISBN = BookQuantity.Text,
                            BookPublication = BookPublication.Text,
                            Language = PurchaseDate.Text,
                            IsAvailable = true,
                            Genre = BookGenre.Text
                        };

                        using (HttpClient client = new HttpClient())
                        {
                            client.BaseAddress = new Uri("https://localhost:5179/api/Book");
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                            var response = await client.PostAsJsonAsync("Book", bookModel);

                            if (response.IsSuccessStatusCode)
                            {
                                MessageBox.Show("Book Added Successfully!");
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show($"Failed to save the book. {response.ReasonPhrase}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid price format. Please enter a valid number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show($"HTTP Request Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please fill in all the required fields.", "Incomplete Data", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



        private bool AreTextBoxesFilled()
        {

            return !string.IsNullOrWhiteSpace(txtBookName.Text) &&
                   !string.IsNullOrWhiteSpace(BookAuthor.Text) &&
                   !string.IsNullOrWhiteSpace(Book_Price.Text) &&
                   !string.IsNullOrWhiteSpace(BookQuantity.Text) &&
                   !string.IsNullOrWhiteSpace(BookPublication.Text) &&
                   PurchaseDate.Text != null;
        }
    }
}
