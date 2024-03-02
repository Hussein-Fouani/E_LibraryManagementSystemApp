using E_LibraryApi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace E_LibraryManagementSystem
{
  
    public partial class ViewBook : Window
    {
        
        public ObservableCollection<BookModel> bookList { get; set; }
        public BookModel bookModel { get; set; }
        public ViewBook()
        {
            InitializeComponent();
            bookModel = new BookModel();
            DataContext = this;
        }
        
        private async Task LoadBooksFromApi(string bookname = null)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response;

                    if (!string.IsNullOrEmpty(bookname))
                    {
                        response = await client.GetAsync($"https://localhost:44360/api/Book?name={bookname}");
                    }
                    else
                    {
                        response = await client.GetAsync("https://localhost:44360/api/Book");
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var books = JsonConvert.DeserializeObject<List<BookModel>>(content);
                        foreach (var book in books)
                        {
                            bookList.Add(book);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Error loading books: {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Can't fetch data: {ex.Message}");
            }
        }


        private async void SearchBook_Click(object sender, RoutedEventArgs e)
        {
            var bookname = SearchBookName.Text;
           await LoadBooksFromApi(bookname);
            bookviewdatagrid.Items.Refresh();

        }

        
      

        private void bookeditdatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure to Delete this  ","Confirmation Dialog",MessageBoxButton.YesNo,MessageBoxImage.Warning,MessageBoxResult.Yes)==MessageBoxResult.Yes)
            {
                if (bookviewdatagrid.SelectedItem != null)
                {
                    BookModel book = (BookModel)bookviewdatagrid.SelectedItem;
                    var bookname = book.BookName;

                    bookList.Remove(book);
                    bookviewdatagrid.Items.Refresh();
                }
            }
            
        }

        private void updatebtn_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure to Update this  ","Update Book",MessageBoxButton.YesNo,MessageBoxImage.Warning,MessageBoxResult.Yes)==MessageBoxResult.Yes)
            {
                if (bookviewdatagrid.SelectedItem != null)
                {
                    BookModel book = (BookModel)bookviewdatagrid.SelectedItem;
                    BookModel model = new BookModel();
                     model.BookName = book.BookName;
                    model.Id = book.Id;
                    model.BookAuthor = book.BookAuthor;
                    model.BookPrice = book.BookPrice;
                    model.BookQuantity = book.BookQuantity;
                    model.BookPublication = book.BookPublication;
                    model.BookPurhcaseDate = book.BookPurhcaseDate;

                    
                    bookviewdatagrid.Items.Refresh();
                }
            }
            
        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            this.bookeditdatagrid.Visibility = Visibility.Hidden;
        }

        private void bookviewdatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (bookviewdatagrid.SelectedItems != null)
            {
                BookModel book = (BookModel)bookviewdatagrid.SelectedItem;
                bookModel.BookName = book.BookName;
                bookModel.BookAuthor = book.BookAuthor;
                bookModel.BookPrice = book.BookPrice;
                bookModel.BookQuantity = book.BookQuantity;
                bookModel.BookPublication = book.BookPublication;
                bookModel.BookPurhcaseDate = book.BookPurhcaseDate;
            }
            if (bookviewdatagrid.SelectedCells.Count > 0)
            {
                var selectedBook = bookviewdatagrid.SelectedItem as BookModel;
                if (selectedBook != null)
                {
                    bookeditdatagrid.Visibility = Visibility.Visible;
                    bookModel = selectedBook;
                    bookeditdatagrid.DataContext = bookModel;
                }
            }
        }
    }
}
