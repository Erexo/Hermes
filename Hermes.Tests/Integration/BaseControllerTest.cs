using Hermes.Api;
using Hermes.Infrastructure.Commands;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xunit.Abstractions;

namespace Hermes.Tests.Integration
{
    public class BaseControllerTest
    {
        protected readonly TestServer _Server;
        protected readonly HttpClient _Client;
        protected readonly ITestOutputHelper _Output;

        public BaseControllerTest(ITestOutputHelper output)
        {
            _Server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _Client = _Server.CreateClient();
            _Output = output;
        }

        protected HttpRequestMessage BuildMessage(HttpMethod method, ICommand command, string uri, string jwt = null)
        {
            string body = JsonConvert.SerializeObject(command);

            var message = new HttpRequestMessage
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json"),
                Method = method,
                RequestUri = new Uri($"http://{_Server.BaseAddress.Host}/{uri}")
            };

            if(jwt != null)
                message.Headers.Add("Authorization", $"Bearer {jwt}");

            return message;
        }
    }
}
