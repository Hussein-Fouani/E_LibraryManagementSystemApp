using System.Windows;

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
        private static bool isIssueBookFormOpen = false;
        private IssueBooks issueBook;
        private static bool isUserProfileFormOpen = false;
        private UserProfile userProfile;
         private string userRole;
        public DashBoardForm(string role)
        {
            InitializeComponent();
            userRole = role;
            UpdateUI();
        }

        private void UpdateUI()
        {
            // Disable the "Add New Book" menu item if the user's role is not "admin"
            AddBook.IsEnabled = userRole == "admin";
        }

        private void _LogOut_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure to Log Out?", "Log Out", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window != this)
                    {
                        window.Close();
                    }
                }
                MainWindow loginForm = new MainWindow();
                loginForm.Show();
                this.Close();
            }
            
        }



        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            if (isAddBookFormOpen && addBookForm != null)
            {
                addBookForm.Activate();
            }
            else
            {
                isAddBookFormOpen = true;
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

                // Create and show the 
                viewbook = new ViewBook(userRole);
                viewbook.Closed += (s, args) => { isViewBookFormOpen = false; };
                viewbook.Show();
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

        

      

        private void User_Info_Click(object sender, RoutedEventArgs e)
        {
            if(isUserProfileFormOpen && userProfile != null)
            {
                userProfile.Activate();
            }
            else
            {
                isUserProfileFormOpen = true;
                userProfile = new UserProfile();
                userProfile.Closed += (s, args) => { isUserProfileFormOpen = false; };
                userProfile.Show();
            }

        }

        private void BookReport(object sender, RoutedEventArgs e)
        {
          CrystalReportWpfApplication.Window1 report = new CrystalReportWpfApplication.Window1();
            report.Show();

        }
    }
}
