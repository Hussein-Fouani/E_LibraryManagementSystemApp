﻿using E_LibraryManagementSystem.Exceptions;
using E_LibraryManagementSystem.Services;
using ELibrary.Domain.Models;
using ELibrary.Domain.NewFolder;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Forms.Design;
using System.Windows.Input;

namespace E_LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ApiServices apiService;
        private bool isSignUpFormOpened = false;
        public MainWindow()
        {

            InitializeComponent();
            apiService = new ApiServices("http://localhost:5179/api/User/");

        }



        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DashBoardForm dashboard = new DashBoardForm();
            dashboard.Show();
        }

        private void Sign_UPBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!isSignUpFormOpened)
            {
                // SignUpForm instance doesn't exist, create a new one
                SignUpForm signUpFormInstance = new SignUpForm();
                signUpFormInstance.Closed += SignUpFormInstance_Closed; // Subscribe to Closed event
                signUpFormInstance.Show();

                // Set the flag to true
                isSignUpFormOpened = true;
            }
            else
            {
                // SignUpForm instance already exists, bring it to the front
                Application.Current.Windows.OfType<SignUpForm>().FirstOrDefault()?.Activate();
            }

            // Close the current window
            this.Close();
        }

        private void SignUpFormInstance_Closed(object sender, EventArgs e)
        {
            // Set the flag to false when SignUpForm is closed
            isSignUpFormOpened = false;
        }

        private async void Sign_InBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Check if username and password are provided
                if (string.IsNullOrEmpty(UserNameText.Text) || string.IsNullOrEmpty(PasswordBox.Password))
                {
                    MessageBox.Show("Please enter both username and password.");
                    return;
                }
                Sign_InBtn.IsEnabled = false;
                // Create a User object with the entered credentials
                UserRL user = new UserRL
                {
                    UserName = UserNameText.Text,
                    Password = PasswordBox.Password
                };

                // Use HttpClient to call the sign-in API
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5179/api/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.PostAsync($"SignIn?username={user.UserName}&password={user.Password}", null);


                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Deserialize the response content to a User object
                        var result = await response.Content.ReadFromJsonAsync<UserRL>();

                        // Check if the result is not null (i.e., sign-in successful)
                        if (result != null)
                        {
                            MessageBox.Show("Login successful!");
                            DashBoardForm dashBoardForm = new DashBoardForm();
                            dashBoardForm.Show();
                            this.Close();
                            // Perform actions after successful login (e.g., navigate to another page)
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password.");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Error: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                Sign_InBtn.IsEnabled = true;
            }
        }


        private void UserNameText_MouseEnter(object sender, MouseEventArgs e)
        {
            if (UserNameText.Text == "UserName")
            {
                UserNameText.Clear();

            }
        }


    }
}