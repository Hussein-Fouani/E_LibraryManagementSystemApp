using E_LibraryApi.Models.Dto;
using System.Windows;
using System.Windows.Controls;

namespace E_LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for ViewStudent.xaml
    /// </summary>
    public partial class ViewStudent : Window
    {
        StudentDto student ;
        public ViewStudent()
        {
            InitializeComponent();
            student = new StudentDto();
        }

        private void updatebtn_Click(object sender, RoutedEventArgs e)
        {
           if(MessageBox.Show("Are you sure you want to update?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if(AreAllInputFieldsAreValid())
                {
                    try
                    {
                        //Update Student
                        Studentviewdatagrid.Items.Refresh();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Empty Boxes Recognized!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void deletebtn_Click(object sender, RoutedEventArgs e)
        {
           if(MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if(AreAllInputFieldsAreValid())
                {
                    string searchterm = SearchStudentName.Text;
                    try
                    {
                        //Delete Student
                        Studentviewdatagrid.Items.Refresh();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Empty Boxes Recognized!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }

        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to cancel?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
        private bool AreAllInputFieldsAreValid()
        {
           foreach(var item in Studentviewdatagrid.Items)
            {
                var row = Studentviewdatagrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                var studenttxtbox = Studentviewdatagrid.Columns[0].GetCellContent(row)?.FindCommonVisualAncestor(studentNameTextBox);
                var studentEmailtxtbox = Studentviewdatagrid.Columns[1].GetCellContent(row)?.FindCommonVisualAncestor(emailtxtbox);
                var departmenttx = Studentviewdatagrid.Columns[2].GetCellContent(row)?.FindCommonVisualAncestor(departmenttxtbox);
                var enrollmentbox = Studentviewdatagrid.Columns[3].GetCellContent(row)?.FindCommonVisualAncestor(Enrollmentnbtxtbox);
                var semesterbox = Studentviewdatagrid.Columns[4].GetCellContent(row)?.FindCommonVisualAncestor(studentsemtxtbox);
                var contactbox = Studentviewdatagrid.Columns[5].GetCellContent(row)?.FindCommonVisualAncestor(contacttxtbox);
                if (studenttxtbox == null || studentEmailtxtbox == null || departmenttx == null || enrollmentbox == null || semesterbox == null || contactbox == null)
                {
                    MessageBox.Show("Please Fill All The Fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            return true;

        }

        private void SearchBook_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SearchStudentName.Text))
            {
                // Set the GIF source to the one with animation
                gifimage.Resources["gifimage"] = new Uri(@"\Images/Studentclass.gif");
                ViewStudentText.Visibility=Visibility.Hidden;
                Studentviewdatagrid.Items.Refresh();
            }
            else
            {
                // Set the GIF source to the one without animation or null if you don't want any
                gifimage.Resources["gifimage"] = new Uri(@"\Images/searchgif.gif");
                ViewStudentText.Visibility = Visibility.Visible;
                Studentviewdatagrid.Items.Refresh();
            }

        }

        private void Studentviewdatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Studentviewdatagrid.SelectedItems.Count > 0)
            {
                Studentditdatagrid.Visibility=Visibility.Visible;
            }
            else
            {
                Studentditdatagrid.Visibility=Visibility.Hidden;
            }
        }

        private void bookeditdatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}