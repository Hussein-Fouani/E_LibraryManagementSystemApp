using ELibrary.Domain.NewFolder;
using Newtonsoft.Json;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace E_LibraryManagementSystem
{

    public partial class ViewStudent : Window
    {
        StudentDto student;
        HttpClient client = new HttpClient();
        public ViewStudent()
        {
            InitializeComponent();
            student = new StudentDto();
            client.BaseAddress = new Uri("https://localhost:7068/api/student/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        private bool AreAllInputFieldsValid()
        {
            foreach (var item in Studentviewdatagrid.Items)
            {
                var row = Studentviewdatagrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;

                if (row != null)
                {
                    foreach (var column in Studentviewdatagrid.Columns)
                    {
                        var cellContent = column.GetCellContent(row);

                        if (cellContent != null)
                        {
                            var text = (cellContent as TextBlock)?.Text;

                            if (string.IsNullOrWhiteSpace(text))
                            {
                                MessageBox.Show("Please fill all the fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        private async void updatebtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to update?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (AreAllInputFieldsValid())
                {
                    try
                    {
                        // Get the selected item (assuming your DataGrid is bound to a collection of objects)
                        var selectedStudent = Studentviewdatagrid.SelectedItem as StudentDto;

                        // Check if a row is selected
                        if (selectedStudent != null)
                        {
                          await  UpdateStudent(GetData());

                            // Refresh the DataGrid to reflect the changes
                            Studentviewdatagrid.Items.Refresh();
                        }
                        else
                        {
                            MessageBox.Show("Please select a row to update.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private async Task UpdateStudent(StudentDto studentDto)
        {
            await client.PutAsJsonAsync($"student/{studentDto.Id}", studentDto);
        }
        private StudentDto GetData()
        {
            StudentDto student = new StudentDto();
            student.StudentName = studentNameTextBox.Text;
            student.StudentEmail = emailtxtbox.Text;
            student.Department = departmenttxtbox.Text;
            student.EnrollmentNb = int.Parse(Enrollmentnbtxtbox.Text);
            student.StudentSemester = studentsemtxtbox.Text;
            student.StudentContact = int.Parse(contacttxtbox.Text);
            return student;
        }
        private async void deletebtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (AreAllInputFieldsValid())
                {
                    
                    try
                    {
                        //Delete Student
                        var selectedStudent = Studentviewdatagrid.SelectedItem as StudentDto;
                        if (selectedStudent != null)
                        {
                            await DeleteStudent(selectedStudent.Id);
                            Studentviewdatagrid.Items.Remove(selectedStudent);
                            Studentviewdatagrid.Items.Refresh();
                        }
                        else
                        {
                            MessageBox.Show("Please select a row to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Empty Boxes Recognized!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }

        }
        private async Task DeleteStudent(Guid id)
        {
            await client.DeleteAsync($"student/{id}");
        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to cancel?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }



        private async void SearchBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(SearchStudentName.Text))
                {
                    // Set the GIF source to the one with animation
                    gifimage.Resources["gifimage"] = new Uri(@"\Images/Studentclass.gif");
                    ViewStudentText.Visibility = Visibility.Hidden;
                    Studentviewdatagrid.Items.Refresh();
                    await GetStudent();
                }
                else
                {
                    // Set the GIF source to the one without animation or null if you don't want any
                    gifimage.Resources["gifimage"] = new Uri(@"\Images/searchgif.gif");
                    ViewStudentText.Visibility = Visibility.Visible;
                    Studentviewdatagrid.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log them, or show an error message to the user
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task GetStudent()
        {
            var response = await client.GetStringAsync($"student/{SearchStudentName.Text}");
            var students = JsonConvert.DeserializeObject<List<StudentDto>>(response);
            Studentviewdatagrid.ItemsSource = students;
        }




        private void Studentviewdatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Studentviewdatagrid.SelectedItem != null)
            {
                Studentditdatagrid.Visibility = Visibility.Visible;
                var student = (StudentDto)Studentviewdatagrid.SelectedItem;
                studentNameTextBox.Text = student.StudentName;
                emailtxtbox.Text = student.StudentEmail;
                departmenttxtbox.Text = student.Department;
                Enrollmentnbtxtbox.Text = student.EnrollmentNb.ToString();
                studentsemtxtbox.Text = student.StudentSemester;
                contacttxtbox.Text = student.StudentContact.ToString();
            }
            else
            {
                Studentditdatagrid.Visibility = Visibility.Hidden;
            }
        }

        private void Studenteditdatagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var student = (StudentDto)Studentviewdatagrid.SelectedItem;
            studentNameTextBox.Text = student.StudentName;
            emailtxtbox.Text = student.StudentEmail;
            departmenttxtbox.Text = student.Department;
            Enrollmentnbtxtbox.Text = student.EnrollmentNb.ToString();
            studentsemtxtbox.Text = student.StudentSemester;
            contacttxtbox.Text = student.StudentContact.ToString();

        }
    }
}