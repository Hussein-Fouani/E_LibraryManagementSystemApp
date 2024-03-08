namespace E_LibraryManagementSystem
{
    // SecurityManager.cs

    public static class SecurityManager
    {
        private static string _token;

        public static void StoreToken(string token)
        {
            // You might want to implement a more secure way to store the token
            _token = token;
        }

        public static string GetToken()
        {
            return _token;
        }
    }

}