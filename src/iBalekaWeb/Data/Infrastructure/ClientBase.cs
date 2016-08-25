using iBalekaWeb.Models.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace iBalekaWeb.Data.Infrastructure
{
    public abstract class ClientBase
    {
        private readonly IApiClient apiClient;

        protected ClientBase(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        protected async Task<TResponse> GetListJsonEncodedContent<TResponse, TModel>(string url)
            where TModel : class
            where TResponse : ListModelResponse<TModel>, new()
        {
            using (var apiResponse = await apiClient.GetJsonEncodedContent<TResponse>(url))
            {
                var response = await GetListJsonResponse<TResponse, TModel>(apiResponse);
                return response;
            }
        }
        protected async Task<TResponse> GetSingleJsonEncodedContent<TResponse, TModel>(string url)
            where TModel : class
            where TResponse : SingleModelResponse<TModel>, new()
        {
            using (var apiResponse = await apiClient.GetJsonEncodedContent<TResponse>(url))
            {
                var response = await GetJsonResponse<TResponse, TModel>(apiResponse);
                return response;
            }
        }
        protected async Task<TResponse> PostEncodedContentWithResponse<TResponse, TModel>(string url, TModel model)
            where TModel : class
            where TResponse : SingleModelResponse<TModel>, new()
        {
            using (var apiResponse = await apiClient.PostJsonEncodedContent(url, model))
            {
                var response = await GetJsonResponse<TResponse, TModel>(apiResponse);
                return response;
            }
        }
        protected async Task<TResponse> PutEncodedContentWithResponse<TResponse, TModel>(string url, TModel model)
            where TModel : class
            where TResponse : SingleModelResponse<TModel>, new()
        {
            using (var apiResponse = await apiClient.PutJsonEncodedContent(url, model))
            {
                var response = await GetJsonResponse<TResponse, TModel>(apiResponse);
                return response;
            }
        }
        protected async Task<TResponse> DeleteEncodedContentWithResponse<TResponse, TModel>(string url)
           where TModel : class
           where TResponse : SingleModelResponse<TModel>, new()
        {
            using (var apiResponse = await apiClient.DeleteJsonEncodedContent<TModel>(url))
            {
                var response = await GetJsonResponse<TResponse, TModel>(apiResponse);
                return response;
            }
        }

        private static async Task<TResponse> GetJsonResponse<TResponse, TModel>(HttpResponseMessage response) where TResponse : SingleModelResponse<TModel>, new()
        {
            var clientResponse = new TResponse();            
            if (response.Content != null)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                clientResponse = JsonConvert.DeserializeObject<TResponse>(responseJson);
            }
            else
            {
                clientResponse.Message = "No Response";
            }

            return clientResponse;
        }
        private static async Task<TResponse> GetListJsonResponse<TResponse, TModel>(HttpResponseMessage response) where TResponse : ListModelResponse<TModel>, new()
        {
            var clientResponse = new TResponse();
            if (response.Content != null)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                clientResponse = JsonConvert.DeserializeObject<TResponse>(responseJson);
            }
            else
            {
                clientResponse.Message = "No Response";
            }

            return clientResponse;
        }
    }
}
