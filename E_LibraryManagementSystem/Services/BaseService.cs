using E_LibraryManagementSystem.Models;
using E_LibraryManagementSystem.Models.APIResponse;
using E_LibraryManagementSystem.Services.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace E_LibraryManagementSystem.Services
{
    public class BaseService : IService
    {
        public ApiReponse responseModel { get; set;}
        public IHttpClientFactory  httpClientFactory { get; set; }

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            this.responseModel = new ApiReponse();
            this.httpClientFactory = httpClientFactory;
        }
        public Task<T> SendAsync<T>(ApiRequests apiRequest)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ELibraryAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.URI);
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),Encoding.UTF8,"application/json");
                }
                switch (apiRequest.ApiTypes)
                {
                    case E_Library_Utilities.ApiTypes.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case E_Library_Utilities.ApiTypes.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case E_Library_Utilities.ApiTypes.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
            }catch(Exception ex)
            {
                responseModel.IsSuccess = false;
                responseModel.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
                var res = JsonConvert.SerializeObject(responseModel);
                var apiResponse = JsonConvert.DeserializeObject<T>(res);
                return Task.FromResult(apiResponse);
            }
        }
    }
}
