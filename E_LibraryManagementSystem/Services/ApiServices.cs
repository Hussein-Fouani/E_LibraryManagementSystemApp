using E_LibraryManagementSystem.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Xps;

namespace E_LibraryManagementSystem.Services
{
    public class ApiServices
    {
        private readonly string baseUrl;
        private readonly HttpClient httpClient;

        public ApiServices(string baseUrl)
        {
            this.baseUrl = baseUrl;
            this.httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> PostAsync<T>(string endpoint, object data, string authToken)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                var response = await httpClient.PostAsync($"{baseUrl}/{endpoint}", content);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request exception
                throw new ApiException($"HTTP Request Error: {ex.Message}");
            }
            catch (JsonException ex)
            {
                // Handle JSON parsing exception
                throw new ApiException($"JSON Parsing Error: {ex.Message}");
            }
        }
    }
}
