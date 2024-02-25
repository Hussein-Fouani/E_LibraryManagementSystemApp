using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace E_LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Sign_UPBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sign_InBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserNameText_MouseEnter(object sender, MouseEventArgs e)
        {
            if(UserNameText.Text == "UserName")
            {
                UserNameText.Clear();
            }
        }
    }
}