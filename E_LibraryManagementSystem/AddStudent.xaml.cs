using E_LibraryManagementSystem.Models;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace E_LibraryManagementSystem
{

    public partial class AddStudent : Window
    {
        Student student;
        HttpClient client = new HttpClient();
        public AddStudent()
        {
            InitializeComponent();
            student= new Student();
            client.BaseAddress = new Uri("https://localhost:7068/api/student/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(AreTextBoxesFilled())
            {
                try
                {
                    addStudent(GetStudentData());
                    
                    
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
        private async void addStudent(Student student)
        {
            await client.PostAsJsonAsync("student", student);
        }
        private Student GetStudentData()
        {
            Student student = new Student();
            student.StudentName = txtStudentName.Text;
            student.StudentEmail = StudentEmail.Text;
            student.Department = Departmenttxtbox.Text;
            student.EnrollmentNb = Convert.ToInt32(EnrollmentNbtxtbox.Text);
            student.StudentSemester = studentSemestertxtbox.Text;
            student.StudentContact = Convert.ToInt32(Student_contacttxtbox.Text);
            return student;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            for(int i=0;i<VisualTreeHelper.GetChildrenCount(StudentSubForm);i++)
            {
                var child = VisualTreeHelper.GetChild(StudentSubForm, i);
                if (child is TextBox)
                {
                    ((TextBox)child).Text = string.Empty;
                }
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("UnSaved Data Will Be Deleted.Are you Sure?","Confirmation Dialog",MessageBoxButton.OKCancel,MessageBoxImage.Warning,MessageBoxResult.Yes)==MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
        private bool AreTextBoxesFilled()
        {
            return StudentSubForm.Children.OfType<TextBox>().All(x => !string.IsNullOrWhiteSpace(x.Text));
        }
    }
}
