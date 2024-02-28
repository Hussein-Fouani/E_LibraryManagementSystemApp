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
    /// Interaction logic for ReturnBook.xaml
    /// </summary>
    public partial class ReturnBook : Window
    {
        public ReturnBook()
        {
            InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (issuebookview.SelectedItems.Count <= 0)
            {
                issueeditpanel.Visibility = Visibility.Hidden;
            }
            else
            {
                issuebookview.Visibility = Visibility.Visible;

            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to cancel?", "Cancel", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
