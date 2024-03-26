using ELibrary.Domain.NewFolder;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;


namespace E_LibraryManagementSystem
{

    public partial class ViewBook : Window
    {
        private readonly HttpClient httpClient = new HttpClient();
        private ObservableCollection<BookDto> bookList = new ObservableCollection<BookDto>();
        private const string BaseApiUrl = "http://localhost:5179/api/";
        private string userrole;

        public ViewBook(string role)
        {
            InitializeComponent();
            userrole = role;
            GetBooksFromApi();
            this.DataContext = bookList;
        }
      

        private async void GetBooksFromApi()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(BaseApiUrl);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await httpClient.GetStringAsync("Book/all");
                    var books = JsonConvert.DeserializeObject<List<BookDto>>(response);

                    if (books != null)
                    {
                        foreach (var book in books)
                        {
                            bookList.Add(book);
                        }
                        bookviewdatagrid.DataContext = books;
                    }
                    else
                    {
                        throw new Exception("Error getting books from API: Empty response");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting books from API: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async void deletebtn_Click(object sender, RoutedEventArgs e)
        {

            if (userrole=="admin")
            {
                if (MessageBox.Show("Are you sure you want to delete this?", "Confirmation Dialog", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.Yes)
                {
                    if (bookviewdatagrid.SelectedItem is BookDto selectedBook)
                    {
                        try
                        {
                            using (HttpClient httpClient = new HttpClient())
                            {
                                httpClient.BaseAddress = new Uri(BaseApiUrl);
                                httpClient.DefaultRequestHeaders.Accept.Clear();
                                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                HttpResponseMessage response = await httpClient.DeleteAsync($"Book/{selectedBook.Id}");

                                if (response.IsSuccessStatusCode)
                                {
                                    // Remove the deleted book from the list
                                    bookList.Remove(selectedBook);
                                    // Refresh the datagrid
                                    bookviewdatagrid.Items.Refresh();

                                    MessageBox.Show("Book deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                else
                                {
                                    MessageBox.Show($"Failed to delete book. Status code: {response.StatusCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
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
                }
            }
            else
            {
                MessageBox.Show("You are not authorized to delete this book", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        private async void updatebtn_Click(object sender, RoutedEventArgs e)
        {
            if (userrole=="admin")
            {

                if (MessageBox.Show("Are you sure to Update this", "Update Book", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (bookviewdatagrid.SelectedItem != null)
                    {
                        BookDto selectedBook = (BookDto)bookviewdatagrid.SelectedItem;

                        BookDto bookDto = ((FrameworkElement)sender).DataContext as BookDto;

                        if (bookDto == null)
                        {
                            MessageBox.Show("No book selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        BookDto updatedBook = new BookDto
                        {
                            Genre = selectedBook.Genre,
                            BookName = selectedBook.BookName,
                            BookAuthor = selectedBook.BookAuthor,
                            BookPrice = selectedBook.BookPrice,
                            ISBN = selectedBook.ISBN,
                            Language = selectedBook.Language,
                            Id = selectedBook.Id,
                            BookPublication = selectedBook.BookPublication
                            // You may need to update other properties based on your model
                        };
                        Guid? bookId = bookList.FirstOrDefault(book => book.BookName == updatedBook.BookName)?.Id;


                        try
                        {
                            using (HttpClient httpClient = new HttpClient())
                            {
                                httpClient.BaseAddress = new Uri(BaseApiUrl);
                                httpClient.DefaultRequestHeaders.Accept.Clear();
                                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                string updateUrl = $"http://localhost:5179/api/Book/{updatedBook.Id}";
                                if (bookId.HasValue)
                                {
                                    var jsonContent = new StringContent(JsonConvert.SerializeObject(updatedBook), Encoding.UTF8, "application/json");
                                    var response = await httpClient.PutAsync(updateUrl, jsonContent);


                                    if (response.IsSuccessStatusCode)
                                    {
                                        // Update the book in the UI
                                        int index = bookList.IndexOf(bookDto);
                                        if (index != -1)
                                        {
                                            bookList[index] = updatedBook;
                                        }

                                        MessageBox.Show("Book updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                                    }
                                    else if (response.StatusCode == HttpStatusCode.NotFound)
                                    {
                                        MessageBox.Show("Book not found on the server.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    }
                                    else
                                    {
                                        var errorContent = await response.Content.ReadAsStringAsync();
                                        MessageBox.Show($"Failed to update book. Status code: {response.StatusCode} Error: {errorContent}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show($"Failed to update book. Status code:", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                } 
            }
            else
            {
                MessageBox.Show("You are not authorized to delete this book", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }





        private async void SearchBook_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = SearchBookName.Text.Trim();
            if (string.IsNullOrEmpty(searchQuery))
            {
                MessageBox.Show("Search query is required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                GetBooksFromApi();
                return;
            }
            else
            {
                await SearchBooks(searchQuery);

                // Provide user feedback after the search
                MessageBox.Show("Search completed.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }

        private async Task SearchBooks(string searchQuery)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(BaseApiUrl);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await httpClient.GetStringAsync($"Book/?query={searchQuery}");
                    var books = JsonConvert.DeserializeObject<List<BookDto>>(response);
                    bookviewdatagrid.DataContext = books;
                    if (response != null)
                    {
                        if (books.Count > 0)
                        {
                            bookList.Clear();
                            foreach (var book in books)
                            {
                                bookList.Add(book);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No books found.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"API request failed with status code {response}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



    }

}

