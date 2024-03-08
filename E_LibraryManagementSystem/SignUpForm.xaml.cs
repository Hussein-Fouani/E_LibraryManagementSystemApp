using ELibrary.Domain.Models;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Input;

namespace E_LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUpForm : Window
    {
        private bool isMainWindowOpened = false;



        public SignUpForm()
        {
            InitializeComponent();
        }


        private void clickmeimage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenGitHubRepository();
        }

        private void OpenGitHubRepository()
        {
            string githubRepositoryUrl = "https://github.com/Hussein-Fouani/E_LibraryManagementSystemApp";
            try
            {
                Process.Start(new ProcessStartInfo(githubRepositoryUrl)
                {
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Opening GitHub Repository: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private async void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Check if username, password, and confirm password are provided
                if (string.IsNullOrEmpty(SignUpUserNamebox.Text) || string.IsNullOrEmpty(SignUpPasswordbox.Password) || string.IsNullOrEmpty(ConfirmPasswordbox.Password)|| string.IsNullOrEmpty(EmailTextBox.Text))
                {
                    MessageBox.Show("Please Fill All Fields Before Submittion.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Check if password matches the confirmation password
                if (SignUpPasswordbox.Password != ConfirmPasswordbox.Password)
                {
                    MessageBox.Show("Password and confirmation password do not match.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Create a User object with the entered credentials
                var user = new SignUp()
                {
                   Username = SignUpUserNamebox.Text,
                    Password = SignUpPasswordbox.Password,
                    ConfirmPassword= ConfirmPasswordbox.Password,
                    Email =EmailTextBox.Text
                };

                // Use HttpClient to call the signup API
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5179/api/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


                    var response = await client.PostAsJsonAsync("SignUp", user);


                    if (response.IsSuccessStatusCode)
                    {

                        var result = await response.Content.ReadFromJsonAsync<bool>();


                        if (result)
                        {
                            MessageBox.Show("Signup successful!");
                            MainWindow login = new MainWindow();
                            login.Show();
                            this.Close();

                        }
                        else
                        {
                            MessageBox.Show("User with the given username already exists.");
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
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isMainWindowOpened)
            {
                // MainWindow instance doesn't exist, create a new one
                MainWindow mainWindowInstance = new MainWindow();
                mainWindowInstance.Closed += MainWindowInstance_Closed; // Subscribe to Closed event
                mainWindowInstance.Show();

                // Set the flag to true
                isMainWindowOpened = true;
            }
            else
            {
                // MainWindow instance already exists, bring it to the front
                Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.Activate();
            }

            // Close the current window
            this.Close();
        }

        private void MainWindowInstance_Closed(object sender, EventArgs e)
        {
            // Set the flag to false when MainWindow is closed
            isMainWindowOpened = false;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string githubRepositoryUrl = "https://github.com/Hussein-Fouani";
            try
            {
                Process.Start(new ProcessStartInfo(githubRepositoryUrl)
                {
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Opening GitHub Repository: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
