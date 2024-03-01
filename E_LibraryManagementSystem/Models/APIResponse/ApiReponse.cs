using System.Net;

namespace E_LibraryManagementSystem.Models.APIResponse
{
    public class ApiReponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;

        public List<string> ErrorMessages { get; set; } = new List<string>();
        public object Result { get; set; }
    }
}
