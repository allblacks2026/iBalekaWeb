using iBalekaWeb.Models.Responses;
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
        SingleModelResponse<TModel> PostContent<TModel>(string requestUri, TModel content) where TModel : class;
        SingleModelResponse<TModel> PutContent<TModel>(string requestUri, TModel content) where TModel : class;
        SingleModelResponse<TModel> DeleteContent<TModel>(string requestUri, int content) where TModel : class;
        SingleModelResponse<TModel> GetSingleContent<TModel>(string requestUri) where TModel : class;
        ListModelResponse<TModel> GetListContent<TModel>(string requestUri) where TModel : class;
        //    Task<HttpResponseMessage> GetJsonEncodedContent(string requestUri);
        //    Task<HttpResponseMessage> PostJsonEncodedContent<T>(string requestUri, T content) where T : class;
        //    Task<HttpResponseMessage> PutJsonEncodedContent<T>(string requestUri, T content) where T : class;
        //    Task<HttpResponseMessage> DeleteJsonEncodedContent(string requestUri, int content);
    }
    public class ApiClient : IApiClient
    {
        private HttpClient httpClient;
        private const string BaseUri = "https://localhost:44349/";
        //private const string BaseUri = "https://ibalekaapi.azurewebsites.net/";
        public ApiClient()
        {

        }
        public SingleModelResponse<TModel> GetSingleContent<TModel>(string requestUri)
            where TModel : class
        {
            using (httpClient = new HttpClient())
            {
                SingleModelResponse<TModel> model = null;
                httpClient.BaseAddress = new Uri(BaseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                var response = httpClient.GetAsync(requestUri)
                    .ContinueWith((taskwithresponse) =>
                        {
                            var resp = taskwithresponse.Result.Content.ReadAsStringAsync();
                            resp.Wait();
                            model = JsonConvert.DeserializeObject<SingleModelResponse<TModel>>(resp.Result);
                        }
                    );
                response.Wait();
                return model;
            }
        }
        public ListModelResponse<TModel> GetListContent<TModel>(string requestUri)
            where TModel : class
        {
            using (httpClient = new HttpClient())
            {
                ListModelResponse<TModel> model = null;
                httpClient.BaseAddress = new Uri(BaseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                var response = httpClient.GetAsync(requestUri)
                    .ContinueWith((taskwithresponse) =>
                    {
                        var resp = taskwithresponse.Result.Content.ReadAsStringAsync();
                        resp.Wait();
                        model = JsonConvert.DeserializeObject<ListModelResponse<TModel>>(resp.Result);
                    }
                    );
                response.Wait();
                return model;
            }
        }
        public SingleModelResponse<TModel> PostContent<TModel>(string requestUri, TModel content)
            where TModel : class
        {
            using (httpClient = new HttpClient())
            {
                SingleModelResponse<TModel> model = null;
                httpClient.BaseAddress = new Uri(BaseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                var response = httpClient.PostAsync(requestUri, new StringContent(JsonConvert.SerializeObject(content), System.Text.Encoding.UTF8, "application/json"))
                    .ContinueWith((taskwithresponse) =>
                    {
                        var resp = taskwithresponse.Result.Content.ReadAsStringAsync();
                        resp.Wait();
                        model = JsonConvert.DeserializeObject<SingleModelResponse<TModel>>(resp.Result);
                    }
                    );
                response.Wait();
                return model;
            }
        }


        public SingleModelResponse<TModel> PutContent<TModel>(string requestUri, TModel content)
            where TModel : class
        {
            using (httpClient = new HttpClient())
            {
                SingleModelResponse<TModel> model = null;
                httpClient.BaseAddress = new Uri(BaseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                var response = httpClient.PutAsync(requestUri, new StringContent(JsonConvert.SerializeObject(content), System.Text.Encoding.UTF8, "application/json"))
                    .ContinueWith((taskwithresponse) =>
                    {
                        var resp = taskwithresponse.Result.Content.ReadAsStringAsync();
                        resp.Wait();
                        model = JsonConvert.DeserializeObject<SingleModelResponse<TModel>>(resp.Result);
                    }
                    );
                response.Wait();
                return model;
            }
        }
        public SingleModelResponse<TModel> DeleteContent<TModel>(string requestUri, int content)
            where TModel : class
        {
            using (httpClient = new HttpClient())
            {
                SingleModelResponse<TModel> model = null;
                httpClient.BaseAddress = new Uri(BaseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");

                var response = httpClient.PutAsync(requestUri, new StringContent(JsonConvert.SerializeObject(content), System.Text.Encoding.UTF8, "application/json"))
                    .ContinueWith((taskwithresponse) =>
                    {
                        var resp = taskwithresponse.Result.Content.ReadAsStringAsync();
                        resp.Wait();
                        model = JsonConvert.DeserializeObject<SingleModelResponse<TModel>>(resp.Result);
                    }
                    );
                response.Wait();
                return model;
            }
        }
        //public async Task<HttpResponseMessage> PostJsonEncodedContent<T>(string requestUri, T content) where T : class
        //{
        //    using (httpClient = new HttpClient())
        //    {
        //        httpClient.BaseAddress = new Uri(BaseUri);
        //        httpClient.DefaultRequestHeaders.Accept.Clear();
        //        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(content), System.Text.Encoding.UTF8, "application/json");
        //        var response = await httpClient.PostAsync(requestUri, contentPost);
        //        response.EnsureSuccessStatusCode();
        //        return response;
        //    }
        //}
        //public async Task<HttpResponseMessage> PutJsonEncodedContent<T>(string requestUri, T content) where T : class
        //{
        //    using (httpClient = new HttpClient())
        //    {
        //        httpClient.BaseAddress = new Uri(BaseUri);
        //        httpClient.DefaultRequestHeaders.Accept.Clear();
        //        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(content), System.Text.Encoding.UTF8, "application/json");
        //        var response = await httpClient.PutAsync(requestUri, contentPost);
        //        response.EnsureSuccessStatusCode();
        //        return response;
        //    }
        //}
        //public async Task<HttpResponseMessage> DeleteJsonEncodedContent(string requestUri, int content)
        //{
        //    using (httpClient = new HttpClient())
        //    {
        //        httpClient.BaseAddress = new Uri(BaseUri);
        //        httpClient.DefaultRequestHeaders.Accept.Clear();
        //       HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(content), System.Text.Encoding.UTF8, "application/json");
        //        var response = await httpClient.PutAsync(requestUri, contentPost);
        //        response.EnsureSuccessStatusCode();
        //        return response;
        //    }
        //}
    }
}
