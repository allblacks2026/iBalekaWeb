using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace iBalekaWeb.Data.Infrastructure
{
    public interface IApiClient
    {
        Task<HttpResponseMessage> GetJsonEncodedContent<T>(string requestUri) where T : class;
        Task<HttpResponseMessage> PostJsonEncodedContent<T>(string requestUri, T content) where T : class;
        Task<HttpResponseMessage> PutJsonEncodedContent<T>(string requestUri, T content) where T : class;
        Task<HttpResponseMessage> DeleteJsonEncodedContent<T>(string requestUri) where T : class;
    }
    public class ApiClient:IApiClient
    {
        private HttpClient httpClient;
        private const string BaseUri = "http://ibalekaapi.azurewebsites.net/";
        public ApiClient()
        {
         
        }
        public async Task<HttpResponseMessage> GetJsonEncodedContent<T>(string requestUri) where T : class
        {
            using (httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(BaseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await httpClient.GetAsync(requestUri);
                return response; 
            }
        }
        public async Task<HttpResponseMessage> PostJsonEncodedContent<T>(string requestUri, T content) where T : class
        {
            using (httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(BaseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await httpClient.PostAsync(requestUri, new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json"));
                return response; 
            }
        }
        public async Task<HttpResponseMessage> PutJsonEncodedContent<T>(string requestUri, T content) where T : class
        {
            using (httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(BaseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await httpClient.PutAsync(requestUri, new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json"));
                return response; 
            }
        }
        public async Task<HttpResponseMessage> DeleteJsonEncodedContent<T>(string requestUri) where T : class
        {
            using (httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(BaseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await httpClient.GetAsync(requestUri);
                return response; 
            }
        }
    }
}
