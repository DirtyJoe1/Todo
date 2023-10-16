using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Todo.Entitites.DataTransferObjects;

namespace Desktop
{
    public abstract class HttpClientDesktop
    {
        private readonly Uri _baseUri;
        protected HttpClientDesktop()
        {
            _baseUri = new Uri("http://45.144.64.179/");
        }
        protected HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = _baseUri;
            return client;
        }
    }
}
