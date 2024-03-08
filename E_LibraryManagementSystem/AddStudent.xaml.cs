using ELibrary.Domain.Models;
using ELibrary.Domain.NewFolder;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace E_LibraryManagementSystem
{

    public partial class AddStudent : Window
    {

        public AddStudent()
        {
            InitializeComponent();


        }

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AreTextBoxesFilled())
            {

                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:5179/api/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        StudentDto student = new StudentDto
                        {
                            StudentName = txtStudentName.Text,
                            StudentEmail = StudentEmail.Text,
                            Department = Departmenttxtbox.Text,
                            StudentSemester = studentSemestertxtbox.Text
                        };

                        if (int.TryParse(EnrollmentNbtxtbox.Text, out int enrollmentNumber))
                        {
                            student.EnrollmentNb = enrollmentNumber;
                        }

                        if (int.TryParse(Student_contacttxtbox.Text, out int studentContact))
                        {
                            student.StudentContact = studentContact;
                        }
                        var response = await client.PostAsJsonAsync("Student", student);
                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Student Added Successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();

                        }

                    }




                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }


            }
            else
            {
                MessageBox.Show("Please Fill All The Fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }




        }


        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!AreTextBoxesFilled())
            {
                this.Close();
            }
            else if (MessageBox.Show("Unsaved Data Will Be Deleted. Are you Sure?", "Confirmation Dialog", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                // Perform asynchronous operations if needed

                this.Close();
            }
        }

        private bool AreTextBoxesFilled()
        {
            return StudentSubForm.Children.OfType<TextBox>().All(x => !string.IsNullOrWhiteSpace(x.Text));
        }
        private void ClearTextBoxes()
        {
            // Find all TextBox controls inside StudentSubForm
            var textBoxes = StudentSubForm.Children.OfType<TextBox>();

            // Set the text of each TextBox to empty
            foreach (var textBox in textBoxes)
            {
                textBox.Text = string.Empty;
            }
        }
    }

}
