using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient : IDisposable
    {
        protected readonly HttpClient _Client;

        protected readonly string _ServiceAddress;

        protected BaseClient(IConfiguration Configuration, string _ServiceAddress)
        {
            _ServiceAddress = _ServiceAddress;

            _Client = new HttpClient { BaseAddress = new Uri(Configuration["ClientAddress"]) };

            _Client.DefaultRequestHeaders.Accept.Clear();
            _Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected async Task<T> GetAsync<T>(string url, CancellationToken Cancel = default) where T : new()
        {
            var response = await _Client.GetAsync(url, Cancel);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<T>(Cancel);
            return new T();
        }

        protected T Get<T>(string url) where T : new() => GetAsync<T>(url).Result;

        protected async Task<HttpResponseMessage> PostAsync<T>(string url, T item, CancellationToken Cancel = default)
        {
            return (await _Client.PostAsJsonAsync(url, item, Cancel)).EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Post<T>(string url, T item) => PostAsync(url, item).Result;

        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T item, CancellationToken Cancel = default)
        {
            return (await _Client.PutAsJsonAsync(url, item, Cancel)).EnsureSuccessStatusCode();
        }

        protected HttpResponseMessage Put<T>(string url, T item) => PutAsync(url, item).Result;

        protected async Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken Cancel = default)
        {
            return await _Client.DeleteAsync(url, Cancel);
        }

        protected HttpResponseMessage Delete(string url) => DeleteAsync(url).Result;

        public void Dispose() => _Client.Dispose();
    }
}
