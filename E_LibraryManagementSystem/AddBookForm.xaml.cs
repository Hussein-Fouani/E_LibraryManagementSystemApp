using E_LibraryApi.Models;
using E_LibraryManagementSystem.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;
using TextBox = System.Windows.Controls.TextBox;

namespace E_LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for AddBookForm.xaml
    /// </summary>
    public partial class AddBookForm : Window
    {
        //  private readonly E_LibDb db;

        public AddBookForm()
        {
            InitializeComponent();
        }
        public AddBookForm(E_LibDb db)
        {
            
           //this.db = db;
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
                    if (book is DatePicker picker)
                    {
                        picker.SelectedDate = DateTime.Now;
                    }

                }
                this.Close();
            }
           
            
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            double pricebox=0.0;
            if (AreTextBoxesFilled())
            {
                try
                {

                    if (double.TryParse(Book_Price.Text, out double price))
                    { 
                        pricebox = price;
                    }
                    else
                    {
                        MessageBox.Show("Invalid price format. Please enter a valid number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    Int32 quantity = Int32.Parse(BookQuantity.Text);

                    if (MessageBox.Show("Are you sure to save this book?", "Save Book", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                    {
                        BookModel bookModel = new BookModel()
                        {
                            BookName = txtBookName.Text,
                            BookAuthor = BookAuthor.Text,
                            BookPrice = pricebox,
                            BookQuantity = quantity,
                            BookPublication = BookPublication.Text,
                            BookPurhcaseDate = PurchaseDate.SelectedDate.Value
                        };
                        MessageBox.Show("Book Saved Successfully", "Save Book", MessageBoxButton.OK, MessageBoxImage.Information);
                        CancelBtn_Click(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                   PurchaseDate.SelectedDate != null;
        }
    }
}
