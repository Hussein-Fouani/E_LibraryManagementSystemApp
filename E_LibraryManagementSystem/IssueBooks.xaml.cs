using E_LibraryApi.Models.Dto;
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
    /// Interaction logic for IssueBooks.xaml
    /// </summary>
    public partial class IssueBooks : Window
    {
        StudentDto student;
        public IssueBooks()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to exit?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to refresh?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
                IssueBooks issueBooks = new IssueBooks();
                issueBooks.Show();
            }
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
           string queryname = Searchtxtbox.Text;

        }
    }
}
