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
    /// Interaction logic for DashBoardForm.xaml
    /// </summary>
    public partial class DashBoardForm : Window
    {

        private static bool isAddBookFormOpen = false;
        private AddBookForm addBookForm;
        private static bool isViewBookFormOpen = false;
        private ViewBook viewbook;
        private static bool isAddStudentFormOpen = false;
        private AddStudent addStudent;
        private static bool isViewStudentFormOpen = false;
        private ViewStudent viewStudent;
        private static bool isIssueBookFormOpen = false;
        private IssueBooks issueBook;

        public DashBoardForm()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _LogOut_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure to Log Out?", "Log Out", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                MainWindow loginForm = new MainWindow();
                loginForm.Show();
                this.Close();
            }
            
        }



        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            // Check if the AddBookForm is already open
            if (isAddBookFormOpen && addBookForm != null)
            {
                // If it's open, bring it to the front
                addBookForm.Activate();
            }
            else
            {
                // If not open, set the flag to true
                isAddBookFormOpen = true;

                // Create and show the AddBookForm
                addBookForm = new AddBookForm();
                addBookForm.Closed += (s, args) => { isAddBookFormOpen = false; };
                addBookForm.Show();
            }
        }


        private void ViewBooks_Click(object sender, RoutedEventArgs e)
        {
            if (isViewBookFormOpen && viewbook != null)
            {
                // If it's open, bring it to the front
                viewbook.Activate();
            }
            else
            {
                // If not open, set the flag to true
                isViewBookFormOpen = true;

                // Create and show the AddBookForm
                viewbook = new ViewBook();
                viewbook.Closed += (s, args) => { isViewBookFormOpen = false; };
                viewbook.Show();
            }
        }

        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            if(isAddStudentFormOpen && addStudent != null)
            {
                addStudent.Activate();
            }
            else
            {
                isAddStudentFormOpen = true;
                addStudent = new AddStudent();
                addStudent.Closed += (s, args) => { isAddStudentFormOpen = false; };
                addStudent.Show();
            }
        }

        private void ViewStudents_Click(object sender, RoutedEventArgs e)
        {
           if(isViewStudentFormOpen && viewStudent != null)
            {
                viewStudent.Activate();
            }
            else
            {
                isViewStudentFormOpen = true;
                viewStudent = new ViewStudent();
                viewStudent.Closed += (s, args) => { isViewStudentFormOpen = false; };
                viewStudent.Show();
            }
        }

        private void IssueBooks(object sender, RoutedEventArgs e)
        {
            if(isIssueBookFormOpen && issueBook != null)
            {
                issueBook.Activate();
            }
            else
            {
                isIssueBookFormOpen = true;
                issueBook = new IssueBooks();
                issueBook.Closed += (s, args) => { isIssueBookFormOpen = false; };
                issueBook.Show();
            }
        }
    }
}
