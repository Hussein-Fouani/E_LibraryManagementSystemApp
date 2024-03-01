using System.Net;

namespace E_LibraryApi.Models.APIResponse
{
    public class ApiReponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }

        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}
