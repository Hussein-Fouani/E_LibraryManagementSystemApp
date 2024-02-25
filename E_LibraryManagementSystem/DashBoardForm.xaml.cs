﻿using System;
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

        }

        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ViewStudents_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
