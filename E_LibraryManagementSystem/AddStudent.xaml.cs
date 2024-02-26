using E_LibraryApi.Models;
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
    /// Interaction logic for AddStudent.xaml
    /// </summary>
    public partial class AddStudent : Window
    {
        Student student;
        public AddStudent()
        {
            InitializeComponent();
            student= new Student();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            student.StudentName = txtStudentName.Text;
            student.StudentEmail = StudentEmail.Text;
            student.Department = Departmenttxtbox.Text;
            student.EnrollmentNb = Convert.ToInt32(EnrollmentNbtxtbox.Text);
            student.StudentSemester = studentSemestertxtbox.Text;
            student.StudentContact = Convert.ToInt32(Student_contacttxtbox.Text);


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
    }
}
