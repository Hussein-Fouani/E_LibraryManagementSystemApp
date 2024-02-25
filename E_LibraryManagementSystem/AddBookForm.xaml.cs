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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace E_LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for AddBookForm.xaml
    /// </summary>
    public partial class AddBookForm : Window
    {
        private readonly E_LibDb db;

        public AddBookForm(E_LibDb db)
        {
            InitializeComponent();
            this.db = db;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach(var book in BookSubForm.Children)
            {
                if(book is TextBox box)
                {
                    box.Clear();
                }
                if(book is DatePicker picker)
                {
                    picker.SelectedDate = DateTime.Now;
                }
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            Int64 price = Int64.Parse(Book_Price.Text);
            Int64 quantity = Int64.Parse(BookQuantity.Text);

            if(MessageBox.Show("Are you sure to save this book?","Save Book",MessageBoxButton.OKCancel,MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                MessageBox.Show("Book Saved Successfully", "Save Book", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            
        }
    }
}
