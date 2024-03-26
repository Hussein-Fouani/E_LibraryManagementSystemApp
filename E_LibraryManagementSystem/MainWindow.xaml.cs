using ELibrary.Domain.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Input;

namespace E_LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isSignUpFormOpened = false;
        public string role { get; set; }
        public MainWindow()
        {

            InitializeComponent();

            
        }

        

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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
                        role = result.Role;

                        // Check if the result is not null (i.e., sign-in successful)
                        if (result != null)
                        {
                            MessageBox.Show("Login successful!","Login",MessageBoxButton.OK,MessageBoxImage.Information);
                            DashBoardForm dashBoardForm = new DashBoardForm(role); // Pass role to DashBoardForm constructor
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
                        MessageBox.Show($"Error Signing In , Check your Credentials");
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

        public string GetRole()
        {
            return role;
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